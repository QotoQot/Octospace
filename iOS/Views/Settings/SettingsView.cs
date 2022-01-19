using Core.ViewModels.Settings;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

using UIKit;

namespace iOS.Views.Settings
{
    public partial class SettingsView : MvxViewController<SettingsViewModel>
    {
        [Export("initWithCoder:")]
        public SettingsView(NSCoder coder) : base(coder) => Initialize();
        public SettingsView(IntPtr handle) : base(handle) => Initialize();
        public SettingsView() : base("SettingsView", NSBundle.MainBundle) => Initialize();
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

