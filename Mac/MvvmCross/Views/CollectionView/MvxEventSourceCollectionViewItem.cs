using MvvmCross.Base;
using System;
using AppKit;
using Foundation;
using MvvmCross.Platforms.Mac.Views.Base;
using Core.Logging;

namespace Mac.MvvmCross.Views
{
    public class MvxEventSourceCollectionViewItem : NSCollectionViewItem, IMvxEventSourceViewController
    {
        protected MvxEventSourceCollectionViewItem() : base() => Initialize();
        protected MvxEventSourceCollectionViewItem(IntPtr handle) : base(handle) => Initialize();
        protected MvxEventSourceCollectionViewItem(NSCoder coder) : base(coder) => Initialize();
        protected MvxEventSourceCollectionViewItem(string nibName, NSBundle bundle) : base(nibName, bundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewDidLoadCalled?.Raise(this);
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            ViewWillAppearCalled?.Raise(this);
        }

        public override void ViewDidLayout()
        {
            base.ViewDidLayout();
            ViewDidLayoutCalled?.Raise(this);
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
