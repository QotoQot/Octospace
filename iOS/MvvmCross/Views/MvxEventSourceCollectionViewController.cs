using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Views.Base;
using System;
using UIKit;

namespace iOS.MvvmCross.Views
{
    public class MvxEventSourceCollectionViewController : UICollectionViewController, IMvxEventSourceViewController
    {
        public MvxEventSourceCollectionViewController() : base() { }
        public MvxEventSourceCollectionViewController(NSCoder coder) : base(coder) { }
        protected MvxEventSourceCollectionViewController(NSObjectFlag t) : base(t) { }
        protected internal MvxEventSourceCollectionViewController(IntPtr handle) : base(handle) { }
        public MvxEventSourceCollectionViewController(string nibName, NSBundle bundle) : base(nibName, bundle) { }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewWillDisappearCalled?.Raise(this, animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            ViewDidAppearCalled?.Raise(this, animated);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            ViewWillAppearCalled?.Raise(this, animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            ViewDidDisappearCalled?.Raise(this, animated);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewDidLoadCalled?.Raise(this);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            ViewDidLayoutSubviewsCalled?.Raise(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeCalled?.Raise(this);
            }
            base.Dispose(disposing);
        }

        public event EventHandler? ViewDidLoadCalled;

        public event EventHandler? ViewDidLayoutSubviewsCalled;

        public event EventHandler<MvxValueEventArgs<bool>>? ViewWillAppearCalled;

        public event EventHandler<MvxValueEventArgs<bool>>? ViewDidAppearCalled;

        public event EventHandler<MvxValueEventArgs<bool>>? ViewDidDisappearCalled;

        public event EventHandler<MvxValueEventArgs<bool>>? ViewWillDisappearCalled;

        public event EventHandler? DisposeCalled;
    }
}
