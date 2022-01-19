using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Core.ViewModels.Settings;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.Platforms.Mac.Binding;
using Mac.MvvmCross.Bindings;
using Core.Logging;

namespace Mac.Views.Settings
{
    public partial class SettingsTextView : MvxViewController<SettingsTextViewModel>
    {
        [Export("initWithCoder:")]
        public SettingsTextView(NSCoder coder) : base(coder) => Initialize();
        public SettingsTextView(IntPtr handle) : base(handle) => Initialize();
        public SettingsTextView() : base(nameof(SettingsTextView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            FontHeader.StringValue = ViewModel.FontHeader;
            UseSemiboldBtn.Title = ViewModel.UseSemiboldLabel;

            PagesHeader.StringValue = ViewModel.PagesHeader;
            PageFontSizeLabel.StringValue = ViewModel.PageFontSizeLabel;
            PageLineSpacingLabel.StringValue = ViewModel.PageLineSpacingLabel;
            PageLineWidthLabel.StringValue = ViewModel.PageLineWidthLabel;

            SpacesHeader.StringValue = ViewModel.SpacesHeader;
            SpaceFontSizeLabel.StringValue = ViewModel.SpaceFontSizeLabel;
            SpaceLineSpacingLabel.StringValue = ViewModel.SpaceLineSpacingLabel;

            EditingHeader.StringValue = ViewModel.EditingHeader;
            CheckOrthographyBtn.Title = ViewModel.CheckOrthographyLabel;
            FetchLinkTitlesBtn.Title = ViewModel.FetchLinkTitlesLabel;
            InsertClosingBracketsBtn.Title = ViewModel.InsertClosingBracketsLabel;

            FontNamePopup.RemoveAllItems();
            FontNamePopup.AddItems(ViewModel.FontNames.ToArray());

            PageLineWidthPopup.RemoveAllItems();
            PageLineWidthPopup.AddItems(ViewModel.PageLineWidthNames.ToArray());

            var set = CreateBindingSet();

            set.Bind(FontNamePopup).For(v => v.BindIndexOfSelectedItem()).To(vm => vm.SelectedFontIndex);
            set.Bind(UseSemiboldBtn).For(v => v.BindState()).To(vm => vm.UseSemibold);

            set.Bind(PageFontSizeBox).For(v => v.BindStringValue()).To(vm => vm.PageFontSize);
            set.Bind(PageLineSpacingBox).For(v => v.BindStringValue()).To(vm => vm.PageLineSpacing);
            set.Bind(PageLineWidthPopup).For(v => v.BindIndexOfSelectedItem()).To(vm => vm.SelectedPageLineWidthIndex);

            set.Bind(SpaceFontSizeBox).For(v => v.BindStringValue()).To(vm => vm.SpaceFontSize);
            set.Bind(SpaceLineSpacingBox).For(v => v.BindStringValue()).To(vm => vm.SpaceLineSpacing);

            set.Bind(CheckOrthographyBtn).For(v => v.BindState()).To(vm => vm.CheckOrthography);
            set.Bind(FetchLinkTitlesBtn).For(v => v.BindState()).To(vm => vm.FetchLinkTitles);
            set.Bind(InsertClosingBracketsBtn).For(v => v.BindState()).To(vm => vm.InsertClosingBrackets);

            set.Apply();
        }
    }
}
