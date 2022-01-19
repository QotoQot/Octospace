using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppKit;
using Core.Logging;
using Foundation;
using Mac.MvvmCross.Presenters.Attributes;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Exceptions;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace Mac.MvvmCross.Presenters
{
    public class MvxAltMacViewPresenter : MvxAttributeViewPresenter, IMvxAltMacViewPresenter
    {
        // TODO: restore size and position
        // https://stackoverflow.com/questions/25150223/nswindowcontroller-autosave-using-storyboard/44140102

        readonly INSApplicationDelegate _applicationDelegate;

        public MvxAltMacViewPresenter(INSApplicationDelegate applicationDelegate)
        {
            _applicationDelegate = applicationDelegate;
            NSWindow.Notifications.ObserveWillClose(HandleWindowWillCloseNotification);
        }

        public NSWindow MainWindow => NSApplication.SharedApplication.MainWindow;
        List<NSWindow> Windows { get; } = new List<NSWindow>();

        public override MvxBasePresentationAttribute CreatePresentationAttribute(Type viewModelType, Type viewType)
            => throw new MvxException($"No presentation attribute set for the view {viewType} ({viewModelType})");

        public override MvxBasePresentationAttribute GetOverridePresentationAttribute(MvxViewModelRequest request, Type viewType)
        {
            if (viewType?.GetInterface(nameof(IMvxOverridePresentationAttribute)) != null)
            {
                var viewInstance = (NSViewController)this.CreateViewControllerFor(viewType, null);
                using (viewInstance)
                {
                    var presentationAttribute = (viewInstance as IMvxOverridePresentationAttribute)?.PresentationAttribute(request);

                    if (presentationAttribute == null)
                        Dlog.Warning("Override PresentationAttribute null. Falling back to existing attribute.");
                    else
                    {
                        if (presentationAttribute.ViewType == null)
                            presentationAttribute.ViewType = viewType;

                        if (presentationAttribute.ViewModelType == null)
                            presentationAttribute.ViewModelType = request.ViewModelType;

                        return presentationAttribute;
                    }
                }
            }

            return null!;
        }
        
        public override void RegisterAttributeTypes()
        {
            AttributeTypesToActionsDictionary.Register<WindowPresentationAttribute>(
                    (viewType, attribute, request) => ShowWindow(attribute, request),
                    (viewModel, attribute) => Close(viewModel));

            AttributeTypesToActionsDictionary.Register<UniqueWindowPresentationAttribute>(
                    (viewType, attribute, request) => ShowWindow(attribute, request),
                    (viewModel, attribute) => Close(viewModel));

            AttributeTypesToActionsDictionary.Register<SheetPresentationAttribute>(
                    (viewType, attribute, request) => ShowSheetModal(attribute, request),
                    (viewModel, attribute) => Close(viewModel));
        }

        public bool ShowPreviouslyOpenedWindow<T>() where T : MvxViewModel
        {
            foreach (var item in Windows)
            {
                if (item.ContentViewController is MvxViewController viewController &&
                    viewController.ViewModel.GetType() == typeof(T))
                {
                    item.MakeKeyAndOrderFront(null);
                    return true;
                }
            }

            return false;
        }

        Task<bool> ShowWindow(WindowPresentationAttribute attribute, MvxViewModelRequest request)
        {
            var viewController = (NSViewController)this.CreateViewControllerFor(request);

            NSWindowController windowController = CreateWindowController(attribute);
            NSWindow window = windowController.Window;

            if (!Windows.Contains(window))
                Windows.Add(window);

            if (!string.IsNullOrEmpty(viewController.Title))
                window.Title = viewController.Title;

            window.ContentView = viewController.View;
            window.ContentViewController = viewController;

            windowController.ShowWindow(null);
            return Task.FromResult(true);
        }

        NSWindowController CreateWindowController(WindowPresentationAttribute attribute)
        {
            if (string.IsNullOrEmpty(attribute.ControllerStoryboardId))
                throw new MvxException("No window controller for ID: " + attribute.ControllerStoryboardId);

            var storyboard = NSStoryboard.FromName("Windows", NSBundle.MainBundle);
            var windowController = (NSWindowController)storyboard.InstantiateControllerWithIdentifier(attribute.ControllerStoryboardId);

            windowController.Window.Identifier = attribute.WindowIdentifier ?? windowController.Window.GetHashCode().ToString();
            return windowController;
        }

        Task<bool> ShowSheetModal(SheetPresentationAttribute attribute, MvxViewModelRequest request)
        {
            var viewController = (NSViewController)this.CreateViewControllerFor(request);
            // by ID for all? get root window IDs somewhere? put into the request?
            throw new NotImplementedException();

            //var window = FindPresentingWindow(attribute.WindowIdentifier, viewController);

            //window.ContentViewController.PresentViewControllerAsSheet(viewController);
            //return Task.FromResult(true);
        }

        NSWindow FindPresentingWindow(string identifier, NSViewController viewController)
        {
            NSWindow? window = null;

            if (!string.IsNullOrEmpty(identifier))
                window = Windows.FirstOrDefault(w => w.Identifier == identifier);

            if (window == null)
                window = MainWindow ?? Windows.LastOrDefault();

            if (window == null)
                throw new MvxException($"Could not find a window with identifier '{identifier}' to display view '{viewController.GetType()}'");

            return window;
        }

        public override Task<bool> Close(IMvxViewModel viewModel)
        {
            for (int i = Windows.Count - 1; i >= 0; i--)
            {
                var window = Windows[i];

                var controller = (MvxViewController)window.ContentViewController;

                // TODO: rewrite both cases with IDs?

                // if closing controller is a sheet, it must have a presenting parent
                var presentedController = controller.PresentedViewControllers?.FirstOrDefault(c => ((MvxViewController)c).ViewModel == viewModel);
                if (presentedController != null)
                {
                    controller.DismissViewController(presentedController);
                    return Task.FromResult(true);
                }

                // closing controller is content in a regular window
                if (controller != null && controller.ViewModel == viewModel)
                {
                    Windows.Remove(window);
                    window.Close();
                    return Task.FromResult(true);
                }

                // TODO: custom tabs
            }

            throw new MvxException($"Could not find and close a view for '{viewModel.GetType()}'");
        }

        void HandleWindowWillCloseNotification(object sender, NSNotificationEventArgs e)
        {
            Guard.IsNotNull(e.Notification.Object, nameof(e.Notification.Object));
            Guard.IsOfType(e.Notification.Object, typeof(NSWindow), nameof(e.Notification.Object));

            var window = (NSWindow)e.Notification.Object;
            if (Windows.Contains(window))
                Windows.Remove(window);
        }
    }
}
