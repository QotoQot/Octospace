using Core.ViewModels.MainWindow;
using Foundation;
using iOS.MvvmCross.Views;
using iOS.Platform;
using MvvmCross.Platforms.Ios.Views;
using System;
using System.Collections.Specialized;
using UIKit;

namespace iOS.Views.MainWindow
{
    public partial class PaneView : MvxViewController<PaneViewModel>
    {
        [Export("initWithCoder:")]
        public PaneView(NSCoder coder) : base(coder) => Initialize();
        public PaneView(IntPtr handle) : base(handle) => Initialize();
        public PaneView() : base("PaneView", NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach (var item in ViewModel.Tabs)
                AddTab(item);

            ViewModel.Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel.Tabs.CollectionChanged -= Tabs_CollectionChanged;
        }

        public bool FocusInPreviouslyOpenedTab(TabContentRequest tabContentRequest)
        {
            // TODO: iterate tabs, check document names, return true if found
            // also, actually focus the input
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
            if (ChildViewControllers.Length != 0)
                throw new NotImplementedException("Can't add more than one tab to a single pane currently!");

            var tab = (SingleTabView)this.CreateViewControllerWithViewModel(singleTabViewModel);
            this.AddChild(tab);
        }
    }
}

