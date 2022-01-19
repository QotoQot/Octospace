using System;
using System.ComponentModel;
using AppKit;
using Core.Logging;
using Core.ViewModels.Content;
using Core.ViewModels.MainWindow;
using CoreGraphics;
using Foundation;
using Mac.MvvmCross.Views;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.ViewModels;

namespace Mac.Views.MainWindow
{
    public partial class SingleTabView : MvxViewController<SingleTabViewModel>
    {
        NSView? _contentView;

        [Export("initWithCoder:")]
        public SingleTabView(NSCoder coder) : base(coder) => Initialize();
        public SingleTabView(IntPtr handle) : base(handle) => Initialize();
        public SingleTabView() : base(nameof(SingleTabView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();

            if (ViewModel.Content != null)
                SetupContent();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();
            //View.Layer.BackgroundColor = CGColor.CreateSrgb(0, 0.7f, 0, 1);
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Content))
            {
                SetupContent();
            }
        }

        void SetupContent()
        {
            if (_contentView != null)
                _contentView.RemoveFromSuperview();

            Guard.IsNotNull(ViewModel.Content, nameof(ViewModel.Content));
            var content = (NSViewController)this.CreateViewControllerWithViewModel(ViewModel.Content);
            _contentView = content.View;
            _contentView.Frame = View.Bounds;
            _contentView.AutoresizingMask = NSViewResizingMask.WidthSizable | NSViewResizingMask.HeightSizable;
            View.AddSubview(_contentView);

            // FUTURE: remove because title should not be used with tabs/panes
            if (((ITabContentViewModel)ViewModel.Content).Title is string title)
                View.Window.Title = title;
        }
    }
}
