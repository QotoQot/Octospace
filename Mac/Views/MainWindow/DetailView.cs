using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using MvvmCross.Platforms.Mac.Views;
using Core.ViewModels.MainWindow;
using System.Collections.Specialized;
using Mac.MvvmCross.Views;
using CoreGraphics;
using Xamarin.Essentials;

namespace Mac.Views.MainWindow
{
    public partial class DetailView : MvxViewController<DetailViewModel>
    {
        readonly List<PaneView> _panes = new();

        [Export("initWithCoder:")]
        public DetailView(NSCoder coder) : base(coder) => Initialize();
        public DetailView(IntPtr handle) : base(handle) => Initialize();
        public DetailView() : base(nameof(DetailView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();

            // TODO: do not re-add already opened
            foreach (var item in ViewModel.Panes)
                AddPane(item);

            ViewModel.Panes.CollectionChanged += Panes_CollectionChanged;

            // TODO: theme color
            View.Layer.BackgroundColor = CGColor.CreateSrgb(1, 1, 1, 1);
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
            ViewModel.Panes.CollectionChanged -= Panes_CollectionChanged;
        }

        public void ShowTabContent(TabContentRequest request)
        {
            ViewModel.ShowTabContent(request);
        }

        public bool FocusInPreviouslyOpenedTab(TabContentRequest tabContentRequest)
        {
            foreach (var item in _panes)
            {
                if (item.FocusInPreviouslyOpenedTab(tabContentRequest))
                    return true;
            }
            return false;
        }


        void Panes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (PaneViewModel item in e.OldItems)
                {
                    // TODO: move, close
                    // TODO: remove views from _panes
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
            var pane = (PaneView)this.CreateViewControllerWithViewModel(paneViewModel);
            _panes.Add(pane);
            // TODO: nested quasi-splitview WITHOUT split view controller
            // TODO: size, order, etc.

            if (DeviceInfo.Version.Major >= 11)
                pane.View.Frame = View.SafeAreaRect;
            else
                pane.View.Frame = new CGRect(CGPoint.Empty, View.Frame.Size);

            pane.View.TranslatesAutoresizingMaskIntoConstraints = true;
            View.AddSubview(pane.View);
        }
    }
}
