using System;

using AppKit;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Mac.Views.Base;

namespace Mac.MvvmCross.Views
{
    public class MvxEventSourceSplitViewController : NSSplitViewController, IMvxEventSourceViewController
    {
        protected MvxEventSourceSplitViewController() : base() => Initialize();
        protected MvxEventSourceSplitViewController(IntPtr handle) : base(handle) => Initialize();
        protected MvxEventSourceSplitViewController(NSCoder coder) : base(coder) => Initialize();
        protected MvxEventSourceSplitViewController(string nibName, NSBundle bundle) : base(nibName, bundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewDidLoadCalled?.Raise(this);
        }

        public override void ViewDidLayout()
        {
            base.ViewDidLayout();
            ViewDidLayoutCalled?.Raise(this);
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            ViewWillAppearCalled?.Raise(this);
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();
            ViewDidAppearCalled?.Raise(this);
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
            ViewWillDisappearCalled?.Raise(this);
        }

        public override void ViewDidDisappear()
        {
            base.ViewDidDisappear();
            ViewDidDisappearCalled?.Raise(this);
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

        public event EventHandler? ViewDidLayoutCalled;

        public event EventHandler? ViewWillAppearCalled;

        public event EventHandler? ViewDidAppearCalled;

        public event EventHandler? ViewDidDisappearCalled;

        public event EventHandler? ViewWillDisappearCalled;

        public event EventHandler? DisposeCalled;
    }
}
