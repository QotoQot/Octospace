using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using Foundation;
using Core.ViewModels.Settings;
using MvvmCross.Platforms.Mac.Views;
using Mac.MvvmCross.Bindings;
using MvvmCross.Platforms.Mac.Binding;

namespace Mac.Views.Settings
{
    public partial class SettingsThemesView : MvxViewController<SettingsThemesViewModel>
    {
        [Export("initWithCoder:")]
        public SettingsThemesView(NSCoder coder) : base(coder) => Initialize();
        public SettingsThemesView(IntPtr handle) : base(handle) => Initialize();
        public SettingsThemesView() : base(nameof(SettingsThemesView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ColorModeLabel.StringValue = ViewModel.ColorModeLabel;
            ContentThemesFollowAppThemesBtn.Title = ViewModel.ContentThemesFollowAppThemesLabel;

            ColorModePopup.RemoveAllItems();
            ColorModePopup.AddItems(ViewModel.ColorModeNames.ToArray());

            var set = CreateBindingSet();

            set.Bind(ContentThemesFollowAppThemesBtn).For(v => v.BindState()).To(vm => vm.ContentThemesFollowAppThemes);
            set.Bind(ColorModePopup).For(v => v.BindIndexOfSelectedItem()).To(vm => vm.SelectedColorModeIndex);

            set.Apply();
        }
    }
}
