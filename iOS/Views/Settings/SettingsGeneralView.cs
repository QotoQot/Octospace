using Core.ViewModels.Settings;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

using UIKit;

namespace iOS.Views.Settings
{
    public partial class SettingsGeneralView : MvxViewController<SettingsGeneralViewModel>
    {
        [Export("initWithCoder:")]
        public SettingsGeneralView(NSCoder coder) : base(coder) => Initialize();
        public SettingsGeneralView(IntPtr handle) : base(handle) => Initialize();
        public SettingsGeneralView() : base("SettingsGeneralView", NSBundle.MainBundle) => Initialize();
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

