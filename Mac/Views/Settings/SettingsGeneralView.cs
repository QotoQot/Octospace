using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Core.ViewModels.Settings;
using MvvmCross.Platforms.Mac.Views;

namespace Mac.Views.Settings
{
    public partial class SettingsGeneralView : MvxViewController<SettingsGeneralViewModel>
    {
        [Export("initWithCoder:")]
        public SettingsGeneralView(NSCoder coder) : base(coder) => Initialize();
        public SettingsGeneralView(IntPtr handle) : base(handle) => Initialize();
        public SettingsGeneralView() : base(nameof(SettingsGeneralView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}
