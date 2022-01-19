using System;
using AppKit;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.ViewModels;

namespace Mac.MvvmCross.Views
{
    public class MvxSplitViewController : MvxEventSourceSplitViewController, IMvxMacView
    {
        public MvxSplitViewController(IntPtr handle) : base(handle) => Initialize();
        public MvxSplitViewController(NSCoder coder) : base(coder) => Initialize();
        public MvxSplitViewController(string viewName, NSBundle bundle) : base(viewName, bundle) => Initialize();
        public MvxSplitViewController(string viewName) : base(viewName, NSBundle.MainBundle) => Initialize();
        public MvxSplitViewController() : base() => Initialize();
        void Initialize() => this.AdaptForBinding();

        public object? DataContext
        {
            get { return BindingContext!.DataContext; }
            set { BindingContext!.DataContext = value; }
        }

        public IMvxViewModel? ViewModel
        {
            get { return (IMvxViewModel?)DataContext; }
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

    public class MvxSplitViewController<TViewModel> : MvxSplitViewController, IMvxMacView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public MvxSplitViewController() { }
        public MvxSplitViewController(IntPtr handle) : base(handle) { }
        public MvxSplitViewController(NSCoder coder) : base(coder) { }
        protected MvxSplitViewController(string nibName, NSBundle bundle) : base(nibName, bundle) { }

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
