using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using System;
using UIKit;

namespace iOS.MvvmCross.Views
{
    public class MvxCollectionViewController : MvxEventSourceCollectionViewController, IMvxIosView
    {
        public MvxCollectionViewController() : base() => this.AdaptForBinding();
        public MvxCollectionViewController(NSCoder coder) : base(coder) => this.AdaptForBinding();
        protected MvxCollectionViewController(NSObjectFlag t) : base(t) => this.AdaptForBinding();
        protected internal MvxCollectionViewController(IntPtr handle) : base(handle) => this.AdaptForBinding();
        public MvxCollectionViewController(string nibName, NSBundle bundle) : base(nibName, bundle) => this.AdaptForBinding();

        public object? DataContext
        {
            get { return BindingContext!.DataContext; }
            set { BindingContext!.DataContext = value; }
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

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewModel?.ViewAppearing();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ViewModel?.ViewAppeared();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel?.ViewDisappearing();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            ViewModel?.ViewDisappeared();
        }

        public override void DidMoveToParentViewController(UIViewController? parent)
        {
            base.DidMoveToParentViewController(parent);
            if (parent == null)
                ViewModel?.ViewDestroy();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject? sender) => throw new NotImplementedException();

        protected virtual void OnViewModelSet() { }
    }

    public class MvxCollectionViewController<TViewModel> : MvxCollectionViewController, IMvxIosView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public MvxCollectionViewController() : base() { }
        public MvxCollectionViewController(NSCoder coder) : base(coder) { }
        public MvxCollectionViewController(string nibName, NSBundle bundle) : base(nibName, bundle) { }
        protected MvxCollectionViewController(NSObjectFlag t) : base(t) { }
        protected internal MvxCollectionViewController(IntPtr handle) : base(handle) { }

        public new TViewModel? ViewModel
        {
            get { return (TViewModel?)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MvxFluentBindingDescriptionSet<IMvxIosView<TViewModel>, TViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<IMvxIosView<TViewModel>, TViewModel>();
        }
    }
}
