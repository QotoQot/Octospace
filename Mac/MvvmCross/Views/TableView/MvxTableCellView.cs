using AppKit;
using CoreGraphics;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Mac.Binding.Views;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.ViewModels;
using System;
using System.Drawing;

namespace Mac.MvvmCross.Views
{
    [Register("MvxAltTableCellView")]
    public abstract class MvxAltTableCellView : MvxView, IMvxMacView
    {
        [Export("initWithCoder:")]
        public MvxAltTableCellView(NSCoder coder) : base(coder) { }
        public MvxAltTableCellView(IntPtr handle) : base(handle) { }
        public MvxAltTableCellView(RectangleF frame) : base(frame) { }
        public MvxAltTableCellView() : base() { }

        public MvxViewModelRequest? Request { get; set; }

        public IMvxViewModel? ViewModel
        {
            get => (IMvxViewModel?)DataContext;
            set
            {
                DataContext = value;
                OnViewModelSet();
            }
        }

        public override void ViewWillMoveToSuperview(NSView? newSuperview)
        {
            base.ViewWillMoveToSuperview(newSuperview);
            ViewModel?.ViewAppearing();
        }

        public override void ViewDidMoveToSuperview()
        {
            base.ViewDidMoveToSuperview();
            ViewModel?.ViewAppeared();
        }

        public override void ViewDidUnhide()
        {
            ViewModel?.ViewAppearing();
            base.ViewDidUnhide();
            ViewModel?.ViewAppeared();
        }

        public override void ViewDidHide()
        {
            ViewModel?.ViewDisappearing();
            base.ViewDidHide();
            ViewModel?.ViewDisappeared();
        }

        public override void RemoveFromSuperview()
        {
            ViewModel?.ViewDisappearing();
            base.RemoveFromSuperview();
            ViewModel?.ViewDisappeared();
        }

        protected virtual void OnViewModelSet() { }
    }

    public class MvxAltTableCellView<TViewModel> : MvxAltTableCellView, IMvxMacView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        [Export("initWithCoder:")]
        public MvxAltTableCellView(NSCoder coder) : base(coder) { }
        public MvxAltTableCellView(IntPtr handle) : base(handle) { }
        public MvxAltTableCellView(RectangleF frame) : base(frame) { }
        public MvxAltTableCellView() { }

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
