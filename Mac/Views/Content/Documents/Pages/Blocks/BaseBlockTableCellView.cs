using System;
using Foundation;
using AppKit;
using Mac.MvvmCross.Views;
using MvvmCross.ViewModels;
using System.Drawing;
using CoreGraphics;
using Core.Logging;
using MvvmCross.Plugin.Messenger;
using Core.ViewModels.Content.Documents;
using Core.MvvmCross.Converters;
using MvvmCross.WeakSubscription;
using System.ComponentModel;

namespace Mac.Views.Content.Documents.Pages
{
    public interface IBlockTableCellView
    {
        CGSize ContentSize { get; }
        CGPoint ContentFocus { get; }

        void FocusContentAt(CGPoint point);
    }

    public abstract class BaseBlockTableCellView<TViewModel> : MvxAltTableCellView<TViewModel>, IBlockTableCellView
        where TViewModel : class, IMvxViewModel, IBlockViewModel
    {
        readonly BlockHandle _handle = new BlockHandle(new RectangleF(0, 0, 8, 12))!;
        readonly BlockOverlay _overlay = new();

        IDisposable? _viewModelPropertyChangedSubscription;

        [Export("initWithCoder:")]
        public BaseBlockTableCellView(NSCoder coder) : base(coder) { }
        public BaseBlockTableCellView(IntPtr handle) : base(handle) { }
        public BaseBlockTableCellView(RectangleF frame) : base(frame) { }
        public BaseBlockTableCellView() { }

        public abstract NSView ContentView { get; }

        public virtual CGSize ContentSize => ContentView != null ? ContentView.Bounds.Size : CGSize.Empty;
        public virtual CGPoint ContentFocus => CGPoint.Empty;

        public abstract void FocusContentAt(CGPoint point);

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            AddSubview(_handle);
            AddSubview(_overlay, NSWindowOrderingMode.Below, ContentView);
            UpdateSelection();

            NSTrackingAreaOptions options = NSTrackingAreaOptions.ActiveInKeyWindow |
                                            NSTrackingAreaOptions.MouseEnteredAndExited |
                                            NSTrackingAreaOptions.InVisibleRect;
            AddTrackingArea(new NSTrackingArea(Bounds, options, this, null));
        }

        protected override void OnViewModelSet()
        {
            if (ViewModel is IMvxNotifyPropertyChanged vm)
                _viewModelPropertyChangedSubscription = vm.WeakSubscribe(ViewModel_PropertyChanged);
        }

        public override void ResizeWithOldSuperviewSize(CGSize oldSize)
        {
            base.ResizeWithOldSuperviewSize(oldSize);
            _overlay.Frame = ContentView.Frame;
        }

        public override void MouseEntered(NSEvent theEvent)
        {
            base.MouseEntered(theEvent);

            if (ViewModel != null && ViewModel.IsSelected == false)
                _handle.Hidden = false;
        }

        public override void MouseExited(NSEvent theEvent)
        {
            base.MouseExited(theEvent);

            if (ViewModel == null || ViewModel.IsSelected == false)
                _handle.Hidden = true;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.IsSelected))
                UpdateSelection();
        }

        void UpdateSelection()
        {
            var isHidden = ViewModel == null || ViewModel.IsSelected == false;
            _handle.Hidden = isHidden;
            _overlay.Hidden = isHidden;

            if (isHidden == false)
                _overlay.Frame = ContentView.Frame;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _viewModelPropertyChangedSubscription?.Dispose();
                _viewModelPropertyChangedSubscription = null;
            }
        }
    }
}
