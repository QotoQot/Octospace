using Core.ViewModels.MainWindow;
using Foundation;
using iOS.MvvmCross.Views;
using iOS.Platform;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Platforms.Ios.Views;
using System;
using System.ComponentModel;
using UIKit;

namespace iOS.Views.MainWindow
{
    // TODO: navigation controller? it's non-generic
    public partial class SingleTabView : MvxViewController<SingleTabViewModel>
    {
        readonly UINavigationController _contentNavigationController = new();

        [Export("initWithCoder:")]
        public SingleTabView(NSCoder coder) : base(coder) => Initialize();
        public SingleTabView(IntPtr handle) : base(handle) => Initialize();
        public SingleTabView() : base("SingleTabView", NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _contentNavigationController.NavigationBarHidden = true;
            this.AddChild(_contentNavigationController);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (ViewModel.Content != null)
                SetupContent();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
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
            Guard.IsNotNull(ViewModel.Content, nameof(ViewModel.Content));
            var content = (UIViewController)this.CreateViewControllerWithViewModel(ViewModel.Content);

            // TODO: solve possible memory overflow
            _contentNavigationController.ShowViewController(content, this);
        }
    }
}

