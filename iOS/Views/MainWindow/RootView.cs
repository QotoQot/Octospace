using Core.Logging;
using Core.ViewModels.MainWindow;
using Foundation;
using iOS.MvvmCross.Views;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using System;
using System.Collections.Specialized;
using UIKit;

namespace iOS.Views.MainWindow
{
    [MvxRootPresentation]
    public partial class RootView : MvxSplitViewController<RootViewModel>
    {
        [Export("initWithCoder:")]
        public RootView(NSCoder coder) : base(coder) => Initialize();
        public RootView(IntPtr handle) : base(handle) => Initialize();
        public RootView() : base() => Initialize();
        //public RootView() : base("RootView", NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Dlog.Info("ROOT view did load");

            // TODO: different for iPad, hor/ver?
            //PreferredDisplayMode = UISplitViewControllerDisplayMode.AllVisible;
            PreferredDisplayMode = UISplitViewControllerDisplayMode.PrimaryOverlay;
            //PreferredSplitBehavior = UISplitViewControllerSplitBehavior.Overlay;

            var sidebar = (SidebarView)this.CreateViewControllerWithViewModel(ViewModel.Sidebar);
            var sidebarNavigation = new UINavigationController(sidebar); // TODO: send

            var detail = (DetailView)this.CreateViewControllerWithViewModel(ViewModel.Detail);
            var detailNavigation = new UINavigationController(detail);

            ViewControllers = new UIViewController[] { sidebarNavigation, detailNavigation };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Dlog.Info("ROOT view will appear");
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}

