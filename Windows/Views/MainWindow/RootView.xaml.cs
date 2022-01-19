using Core.Logging;
using Core.ViewModels.MainWindow;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.UI.Xaml.Controls;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using System;
using Win.Views.Settings;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Win.Views.MainWindow
{
#nullable enable
    public abstract class RootViewAbstract : MvxWindowsPage<RootViewModel> { }

    [MvxPagePresentation]
    public sealed partial class RootView : RootViewAbstract//, IMvxOverridePresentationAttribute
    {
        SidebarView _sidebar = null!;

        public RootView()
        {
            InitializeComponent();
            SetupCustomTitleBar();

            Loading += Page_Loading;
            Unloaded += Page_Unloaded;
        }

        void Page_Loading(FrameworkElement sender, object args)
        {
            Guard.IsNotNull(ViewModel.Sidebar, nameof(ViewModel.Sidebar));
            _sidebar = new SidebarView { ViewModel = ViewModel.Sidebar };
            _sidebar.NavigationView.SelectionChanged += NavigationView_SelectionChanged;
            _grid.Children.Add(_sidebar);

            Guard.IsNotNull(ViewModel.Detail, nameof(ViewModel.Detail));
            _sidebar.ShowContent(ViewModel.Detail);
        }

        void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_sidebar != null && _sidebar.NavigationView != null)
                _sidebar.NavigationView.SelectionChanged -= NavigationView_SelectionChanged;
        }


        #region Navigation

        // Update the TitleBar content layout depending on NavigationView DisplayMode
        void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
        {
            int topIndent = 16;
            int expandedIndent = 48;
            int minimalIndent = 104;

            if (_sidebar.NavigationView.IsBackButtonVisible == NavigationViewBackButtonVisible.Collapsed)
                minimalIndent = 48;

            Thickness currMargin = _titleBar.Margin;

            // Set the TitleBar margin dependent on NavigationView display mode
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                _titleBar.Margin = new Thickness(topIndent, currMargin.Top, currMargin.Right, currMargin.Bottom);
            }
            else if (sender.DisplayMode == NavigationViewDisplayMode.Minimal)
            {
                _titleBar.Margin = new Thickness(minimalIndent, currMargin.Top, currMargin.Right, currMargin.Bottom);
            }
            else
            {
                _titleBar.Margin = new Thickness(expandedIndent, currMargin.Top, currMargin.Right, currMargin.Bottom);
            }
        }

        // FUTURE: NewWindowPresentationAttribute for ctrl+N
        //public MvxBasePresentationAttribute PresentationAttribute(MvxViewModelRequest request)
        void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is SidebarOutlineItemViewModel)
            {
                var item = (SidebarOutlineItemViewModel)args.SelectedItem;

                if (_sidebar.ContentFrame.Content is SettingsView)
                    _sidebar.ContentFrame.GoBack();

                //var contentType = TabContentType.Page;
                //ViewModel.Detail.ShowTabContent(new TabContentRequest(TabDisplayMode.ReplaceFocused, contentType));
            }
            else if (args.IsSettingsSelected)
                _sidebar.ShowContent(ViewModel.CreateSettings());
            else
                throw new ArgumentException($"Unknown selected item in navigation view: {args.SelectedItem}");
        }

        #endregion


        #region Title Bar

        void SetupCustomTitleBar()
        {
            var appTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            appTitleBar.ButtonBackgroundColor = Colors.Transparent;
            appTitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            UpdateTitleBarLayout(coreTitleBar);

            Window.Current.SetTitleBar(_titleBar);

            // replicate the behavior of the hidden default title bar
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
            Window.Current.Activated += CurrentWindow_Activated;
        }

        void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            // Update title bar control size as needed to account for system size changes.
            _titleBar.Height = coreTitleBar.Height;

            // Ensure the custom title bar does not overlap window caption controls
            Thickness margin = _titleBar.Margin;
            _titleBar.Margin = new Thickness(margin.Left, margin.Top, coreTitleBar.SystemOverlayRightInset, margin.Bottom);
        }

        void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args) => UpdateTitleBarLayout(sender);

        void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
                _titleBar.Visibility = Visibility.Visible;
            else
                _titleBar.Visibility = Visibility.Collapsed;
        }

        void CurrentWindow_Activated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == CoreWindowActivationState.Deactivated)
                _titleBarTitle.Foreground = (SolidColorBrush)Application.Current.Resources["TextFillColorDisabledBrush"];
            else
                _titleBarTitle.Foreground = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.LayoutMetricsChanged -= CoreTitleBar_LayoutMetricsChanged;
                coreTitleBar.IsVisibleChanged -= CoreTitleBar_IsVisibleChanged;

                Window.Current.Activated -= CurrentWindow_Activated;

                Loading -= Page_Loading;
                Unloaded -= Page_Unloaded;
            }
        }
    }
#nullable restore
}
