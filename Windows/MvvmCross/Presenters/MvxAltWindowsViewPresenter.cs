using Core.Logging;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Uap.Presenters;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Win.MvvmCross.Presenters
{
    public class MvxAltWindowsViewPresenter : MvxAttributeViewPresenter, IMvxWindowsViewPresenter
    {
        readonly IMvxWindowsFrame _rootFrame;

        public MvxAltWindowsViewPresenter(IMvxWindowsFrame rootFrame)
        {
            _rootFrame = rootFrame;
            // FUTURE: GetForCurrentView does not work in multi-window environment
            SystemNavigationManager.GetForCurrentView().BackRequested += OnSystemBackBtn;
        }

        IMvxViewModelLoader _viewModelLoader;
        public IMvxViewModelLoader ViewModelLoader
        {
            get
            {
                if (_viewModelLoader == null)
                    _viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
                return _viewModelLoader;
            }
            set => _viewModelLoader = value;
        }

        public override void RegisterAttributeTypes()
        {
            // FUTURE: finish for on-the-fly attribute for ctrl+N
            //AttributeTypesToActionsDictionary.Register<NewWindowPresentationAttribute>(
            //        (viewType, attribute, request) => ShowWindow(attribute, request),
            //        (viewModel, attribute) => CloseWindow(viewModel));

            AttributeTypesToActionsDictionary.Register<MvxPagePresentationAttribute>(
                    (viewType, attribute, request) => ShowPage(viewType, request),
                    (viewModel, attribute) => ClosePage(viewModel));

            //AttributeTypesToActionsDictionary.Register<TabContentPresentationAttribute>(
            //        (viewType, attribute, request) => ShowTabContent(request),
            //        (viewModel, attribute) => CloseTabContent(viewModel));

            //AttributeTypesToActionsDictionary.Register<MvxDialogViewPresentationAttribute>(
            //        (viewType, attribute, request) => ShowDialog(viewType, attribute, request),
            //        (viewModel, attribute) => CloseDialog(viewModel, attribute));

            // FUTURE: sheet-like ContentDialog for page/space configuration, etc.
        }

        public override MvxBasePresentationAttribute CreatePresentationAttribute(Type viewModelType, Type viewType)
            => throw new MvxException($"No presentation attribute set for the view {viewType} ({viewModelType})");

        string GetRequestText(MvxViewModelRequest request)
        {
            var requestTranslator = Mvx.IoCProvider.Resolve<IMvxWindowsViewModelRequestTranslator>();

            if (request is MvxViewModelInstanceRequest instanceRequest)
                return requestTranslator.GetRequestTextWithKeyFor(instanceRequest.ViewModelInstance);
            else
                return requestTranslator.GetRequestTextFor(request);
        }

        async void OnSystemBackBtn(object sender, BackRequestedEventArgs backRequestedEventArgs)
        {
            if (backRequestedEventArgs.Handled)
                return;

            if (_rootFrame.Content is not IMvxView currentView)
            {
                Dlog.Warning("Ignoring close for view model because the root frame has no current page");
                return;
            }

            var navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
            backRequestedEventArgs.Handled = await navigationService.Close(currentView.ViewModel);
        }

        void UpdateBackBtnVisibility()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                _rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        Task<bool> ShowPage(Type viewType, MvxViewModelRequest request)
        {
            try
            {
                var requestText = GetRequestText(request);
                var viewsContainer = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();

                // Frame won't allow serialization of its nav-state if it gets a non-simple type as a nav param
                _rootFrame.Navigate(viewType, requestText);

                UpdateBackBtnVisibility();
                return Task.FromResult(true);
            }
            catch (Exception exception)
            {
                Dlog.Info($"Error during navigation request to {request.ViewModelType.Name}: {exception.ToLongString()}");
                return Task.FromResult(false);
            }
        }

        Task<bool> ClosePage(IMvxViewModel viewModel)
        {
            if (_rootFrame.Content is not IMvxView currentView)
            {
                Dlog.Warning("Ignoring close for view model because the root frame has no current page");
                return Task.FromResult(false);
            }

            if (currentView.ViewModel != viewModel)
            {
                Dlog.Warning("Ignoring close for viewmodel because the root frame's current page is not the view for the requested viewmodel");
                return Task.FromResult(false);
            }

            if (_rootFrame.CanGoBack == false)
            {
                Dlog.Warning("Ignoring close for viewmodel - the root frame refuses to go back");
                return Task.FromResult(false);
            }

            _rootFrame.GoBack();
            UpdateBackBtnVisibility();

            return Task.FromResult(true);
        }

        //Task<bool> ShowTabContent(MvxViewModelRequest request)
        //{
            //var requestText = GetRequestText(request);

            //var containerView = _rootFrame.UnderlyingControl.FindControl<Frame>(viewType.GetRegionName());

            //if (containerView != null)
            //{
            //    containerView.Navigate(viewType, requestText);
            //}

        //    return Task.FromResult(true);
        //}

        //Task<bool> CloseTabContent(IMvxViewModel viewModel, MvxRegionPresentationAttribute attribute)
        //{
        //    var viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
        //    var viewType = viewFinder.GetViewType(viewModel.GetType());
        //    var containerView = _rootFrame.UnderlyingControl?.FindControl<Frame>(viewType.GetRegionName());

        //    if (containerView == null)
        //        throw new MvxException($"Region '{viewType.GetRegionName()}' not found in view '{viewType}'");

        //    if (containerView.CanGoBack)
        //    {
        //        containerView.GoBack();
        //        return Task.FromResult(true);
        //    }

        //    return Task.FromResult(true);
        //}

        //async Task<bool> ShowDialog(Type viewType, MvxDialogViewPresentationAttribute attribute, MvxViewModelRequest request)
        //{
        //    try
        //    {
        //        var contentDialog = (ContentDialog)CreateControl(viewType, request, attribute);
        //        if (contentDialog != null)
        //        {
        //            await contentDialog.ShowAsync(attribute.Placement);
        //            return true;
        //        }

        //        return false;
        //    }
        //    catch (Exception exception)
        //    {
        //        Dlog.Info($"Error seen during navigation request to {request.ViewModelType.Name} - error {exception.ToLongString()}");
        //        return false;
        //    }
        //}

        //Task<bool> CloseDialog(IMvxViewModel viewModel, MvxBasePresentationAttribute attribute)
        //{
        //    var popups = VisualTreeHelper.GetOpenPopups(Window.Current).FirstOrDefault(p =>
        //    {
        //        if (attribute.ViewType.IsAssignableFrom(p.Child.GetType())
        //            && p.Child is IMvxWindowsContentDialog dialog)
        //        {
        //            return dialog.ViewModel == viewModel;
        //        }
        //        return false;
        //    });

        //    (popups?.Child as ContentDialog)?.Hide();

        //    return Task.FromResult(true);
        //}

        //Control CreateControl(Type viewType, MvxViewModelRequest request, MvxBasePresentationAttribute attribute)
        //{
        //    try
        //    {
        //        var control = Activator.CreateInstance(viewType) as Control;
        //        if (control is IMvxView mvxControl)
        //        {
        //            if (request is MvxViewModelInstanceRequest instanceRequest)
        //                mvxControl.ViewModel = instanceRequest.ViewModelInstance;
        //            else
        //                mvxControl.ViewModel = ViewModelLoader.LoadViewModel(request, null);
        //        }

        //        return control;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MvxException(ex, $"Cannot create Control '{viewType.FullName}'. Are you use a wrong base class?");
        //    }
        //}
    }
}
