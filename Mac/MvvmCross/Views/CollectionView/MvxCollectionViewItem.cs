using System;
using AppKit;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.ViewModels;

namespace Mac.MvvmCross.Views
{
    public class MvxCollectionViewItem : MvxEventSourceCollectionViewItem, IMvxMacView
    {
        public MvxCollectionViewItem(IntPtr handle) : base(handle) => Initialize();
        public MvxCollectionViewItem(NSCoder coder) : base(coder) => Initialize();
        public MvxCollectionViewItem(string viewName, NSBundle bundle) : base(viewName, bundle) => Initialize();
        public MvxCollectionViewItem(string viewName) : base(viewName, NSBundle.MainBundle) => Initialize();
        public MvxCollectionViewItem() : base() => Initialize();
        void Initialize()
        {
            // not using the usual `this.AdaptForBinding();` with `MvxViewControllerAdapter`
            // because it calls `MacView.OnViewCreate();` which tries to load a view model
            // but view models for collection items are created manually
            new MvxCollectionViewItemAdapter(this);
            new MvxBindingViewControllerAdapter(this); // creates binding context
        }

        public object? DataContext
        {
            get => BindingContext!.DataContext;
            set => BindingContext!.DataContext = value;
        }

        public IMvxViewModel? ViewModel
        {
            get => (IMvxViewModel?)DataContext;
            set
            {
                DataContext = value;
                OnViewModelSet();
            }
        }

        public MvxViewModelRequest? Request { get; set; }

        public IMvxBindingContext? BindingContext { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel?.ViewCreated();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            ViewModel?.ViewAppearing();
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();
            ViewModel?.ViewAppeared();
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
            ViewModel?.ViewDisappearing();
        }

        public override void ViewDidDisappear()
        {
            base.ViewDidDisappear();
            ViewModel?.ViewDisappeared();
        }

        public override void RemoveFromParentViewController()
        {
            base.RemoveFromParentViewController();
            ViewModel?.ViewDestroy();
        }

        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender) => throw new NotImplementedException();

        protected virtual void OnViewModelSet() { }
    }

    public class MvxCollectionViewItem<TViewModel> : MvxCollectionViewItem, IMvxMacView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public MvxCollectionViewItem() { }
        public MvxCollectionViewItem(IntPtr handle) : base(handle) { }
        public MvxCollectionViewItem(NSCoder coder) : base(coder) { }
        protected MvxCollectionViewItem(string nibName, NSBundle bundle) : base(nibName, bundle) { }

        public new TViewModel? ViewModel
        {
            get { return (TViewModel?)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MvxFluentBindingDescriptionSet<IMvxMacView<TViewModel>, TViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<IMvxMacView<TViewModel>, TViewModel>();
        }
    }
}
