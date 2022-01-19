using System;
using AppKit;
using Core.Logging;
using Core.ViewModels.MainWindow;
using CoreGraphics;
using Foundation;
using Mac.MvvmCross.Presenters.Attributes;
using Mac.MvvmCross.Views;
using Microsoft.Toolkit.Diagnostics;

namespace Mac.Views.MainWindow
{
    [WindowPresentation("RootWindowCtrl")]
    public partial class RootView : MvxSplitViewController<RootViewModel>//, INSSplitViewDelegate
    {
        RootToolbar? _toolbarDelegate;
        SidebarView _sidebar = null!;
        DetailView _detail = null!;

        // TODO: preserve user settings
        readonly int _sidebarWidth = 200;

        [Export("initWithCoder:")]
        public RootView(NSCoder coder) : base(coder) => Initialize();
        public RootView(IntPtr handle) : base(handle) => Initialize();
        public RootView() : base(nameof(RootView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Dlog.Info("ROOT view did load");

            //SplitView.Delegate = this;
            SetupSidebar();
            SetupDetail();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            Dlog.Info("ROOT view will appear");

            if (_toolbarDelegate == null && View.Window is NSWindow window
                && window.Toolbar is NSToolbar toolbar)
            {
                _toolbarDelegate = new(window);
                _toolbarDelegate.ItemActivated += ToolbarDelegate_ItemActivated;
            }

            SplitView.SetPositionOfDivider(_sidebarWidth, 0);
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
        }

        public void ShowTabContent(TabContentRequest request) => _detail.ShowTabContent(request);

        public bool FocusInPreviouslyOpenedTab(TabContentRequest request) => _detail.FocusInPreviouslyOpenedTab(request);

        void SetupSidebar()
        {
            var sidebarContainerController = new NSViewController();
            sidebarContainerController.View = SidebarContainer;

            var sidebarItem = NSSplitViewItem.CreateSidebar(sidebarContainerController);
            sidebarItem.CanCollapse = true;
            sidebarItem.MinimumThickness = 160;
            sidebarItem.MaximumThickness = 400;
            AddSplitViewItem(sidebarItem);

            Guard.IsNotNull(ViewModel, nameof(ViewModel));

            _sidebar = (SidebarView)this.CreateViewControllerWithViewModel(ViewModel.Sidebar);
            // TODO: move to ViewWillAppear?
            _sidebar.View.Frame = new CGRect(CGPoint.Empty, SidebarContainer.Frame.Size);
            SidebarContainer.AddSubview(_sidebar.View);
        }

        void SetupDetail()
        {
            var detailContainerController = new NSViewController();
            detailContainerController.View = DetailContainer;

            var detailItem = NSSplitViewItem.FromViewController(detailContainerController);
            detailItem.ViewController = detailContainerController;
            detailItem.CanCollapse = false;
            detailItem.MinimumThickness = 360; // TODO: share with View.Window.MinSize.Width which is null here
            AddSplitViewItem(detailItem);

            Guard.IsNotNull(ViewModel, nameof(ViewModel));

            _detail = (DetailView)this.CreateViewControllerWithViewModel(ViewModel.Detail);
            // TODO: move to ViewWillAppear?
            _detail.View.Frame = new CGRect(CGPoint.Empty, DetailContainer.Frame.Size);
            _detail.View.TranslatesAutoresizingMaskIntoConstraints = true;
            DetailContainer.AddSubview(_detail.View);
        }

        void ToolbarDelegate_ItemActivated(object sender, EventArgs e)
        {
            var item = (NSToolbarItem)sender;

            if (item.Identifier == RootToolbar.ToggleSidebarId)
            {
                //SplitViewItems[0].Collapsed = SplitViewItems[0].Collapsed == false;
                // OR
                //if (SplitView.IsSubviewCollapsed(SidebarContainer))
                //    SplitView.SetPositionOfDivider(_sidebarWidth, 0);
                //else
                //    SplitView.SetPositionOfDivider(SplitView.MinPositionOfDivider(0), 0);
            }
            else if (item.Identifier == RootToolbar.BackId)
            {

            }
            else if (item.Identifier == RootToolbar.ForwardId)
            {

            }
            else if (item.Identifier == RootToolbar.NewDocumentId)
            {

            }
            else if (item.Identifier == RootToolbar.FormattingPanelId)
            {
                
            }
            else if (item.Identifier == RootToolbar.DocumentSettingsId)
            {

            }
            else
                throw new ArgumentException($"Incorrect root toolbar item ID: {item.Identifier}");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_toolbarDelegate != null)
                    _toolbarDelegate.ItemActivated -= ToolbarDelegate_ItemActivated;
            }

            base.Dispose(disposing);
        }
    }
}
