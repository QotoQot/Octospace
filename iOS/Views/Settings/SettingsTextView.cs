using Core.ViewModels.Settings;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

using UIKit;

namespace iOS.Views.Settings
{
    public partial class SettingsTextView : MvxViewController<SettingsTextViewModel>
    {
        [Export("initWithCoder:")]
        public SettingsTextView(NSCoder coder) : base(coder) => Initialize();
        public SettingsTextView(IntPtr handle) : base(handle) => Initialize();
        public SettingsTextView() : base("SettingsTextView", NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

        }
    }
}

