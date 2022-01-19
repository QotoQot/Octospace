using Core.Logging;
using Core.ViewModels.Content;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;

using UIKit;

namespace iOS.Views.Content
{
    [Register("PageView")]
    public partial class PageView : MvxViewController<PageViewModel>
    {
        [Export("initWithCoder:")]
        public PageView(NSCoder coder) : base(coder) => Initialize();
        public PageView(IntPtr handle) : base(handle) => Initialize();
        public PageView() : base("PageView", NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<PageView, PageViewModel>();
            set.Bind(TempText).To(vm => vm.TempText);
            set.Apply();

            //View!.BackgroundColor = UIColor.Green;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

        }
    }
}

