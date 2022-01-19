using System;
using System.Collections.Specialized;
using AppKit;
using Core.ViewModels.Content;
using Core.ViewModels.MainWindow;
using CoreGraphics;
using Foundation;
using Mac.MvvmCross.Views;
using MvvmCross.Platforms.Mac.Views;

namespace Mac.Views.MainWindow
{
    public partial class PaneView : MvxViewController<PaneViewModel>
    {
        [Export("initWithCoder:")]
        public PaneView(NSCoder coder) : base(coder) => Initialize();
        public PaneView(IntPtr handle) : base(handle) => Initialize();
        public PaneView() : base(nameof(PaneView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();

            // TODO: do not re-add already opened
            foreach (var item in ViewModel.Tabs)
                AddTab(item);

            ViewModel.Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
            ViewModel.Tabs.CollectionChanged -= Tabs_CollectionChanged;
        }

        public bool FocusInPreviouslyOpenedTab(TabContentRequest tabContentRequest)
        {
            // TODO: iterate tabs, check document names, return true if found
            return false;
        }

        void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (SingleTabViewModel item in e.OldItems)
                {
                    // TODO: move, close
                }
            }

            if (e.NewItems != null)
            {
                foreach (SingleTabViewModel item in e.NewItems)
                {
                    AddTab(item);
                }
            }
        }

        void AddTab(SingleTabViewModel singleTabViewModel)
        {
            if (View.Subviews.Length != 0)
                throw new NotImplementedException("Can't add more than one tab to a single pane currently!");

            var tab = (SingleTabView)this.CreateViewControllerWithViewModel(singleTabViewModel);
            tab.View.Frame = View.Bounds;
            tab.View.AutoresizingMask = NSViewResizingMask.WidthSizable | NSViewResizingMask.HeightSizable;
            View.AddSubview(tab.View);
        }
    }
}
