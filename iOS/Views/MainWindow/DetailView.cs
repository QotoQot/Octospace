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
    public partial class DetailView : MvxViewController<DetailViewModel>
    {
        [Export("initWithCoder:")]
        public DetailView(NSCoder coder) : base(coder) => Initialize();
        public DetailView(IntPtr handle) : base(handle) => Initialize();
        public DetailView() : base("DetailView", NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach (var item in ViewModel.Panes)
                AddPane(item);

            ViewModel.Panes.CollectionChanged += Panes_CollectionChanged;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel.Panes.CollectionChanged -= Panes_CollectionChanged;
        }

        public void ShowTabContent(TabContentRequest request)
        {
            ViewModel.ShowTabContent(request);
        }

        public bool FocusInPreviouslyOpenedTab(TabContentRequest tabContentRequest)
        {
            //foreach (var item in SplitViewItems)
            //{
            //    if (item.ViewController is PaneView paneView)
            //    {
            //        if (paneView.FocusInPreviouslyOpenedTab(tabContentRequest))
            //            return true;
            //    }
            //}
            return false;
        }

        void Panes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (PaneViewModel item in e.OldItems)
                {
                    // TODO: move, close
                }
            }

            if (e.NewItems != null)
            {
                foreach (PaneViewModel item in e.NewItems)
                {
                    AddPane(item);
                }
            }
        }

        void AddPane(PaneViewModel paneViewModel)
        {
            if (ChildViewControllers.Length != 0)
                throw new NotImplementedException("Can't add more than one pane currently!");

            var pane = (PaneView)this.CreateViewControllerWithViewModel(paneViewModel);
            this.AddChild(pane);
        }
    }
}

