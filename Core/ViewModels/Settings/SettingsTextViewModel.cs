using System;
using System.Collections.Generic;
using Core.Logging;
using Core.Model.State;
using Core.Resources.Fonts;
using Core.Resources.Localization;
using MvvmCross.Binding.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Essentials;

namespace Core.ViewModels.Settings
{
    public class SettingsTextViewModel : SettingsSectionViewModel
    {
        readonly IState _state;

        public SettingsTextViewModel(IState state)
            : base(TextProvider.Get(ResX.Settings, "Text.Tab.Title"))
        {
            _state = state;
        }

        bool UseRegularLabels => DeviceInfo.Platform == DevicePlatform.UWP;

        public string FontHeader => TextProvider.Get(ResX.Settings, "Text.Fonts.Header");
        public string UseSemiboldLabel => TextProvider.Get(ResX.Settings, "Text.Fonts.UseSemibold.Label");

        public string PagesHeader => TextProvider.Get(ResX.Settings, "Text.Pages.Header");
        public string PageFontSizeLabel => UseRegularLabels
            ? TextProvider.Get(ResX.Settings, "Text.Pages.FontSize.Label")
            : TextProvider.Get(ResX.Settings, "Text.Pages.FontSize.Label.Trailing");
        public string PageLineSpacingLabel => UseRegularLabels
            ? TextProvider.Get(ResX.Settings, "Text.Pages.LineSpacing.Label")
            : TextProvider.Get(ResX.Settings, "Text.Pages.LineSpacing.Label.Trailing");
        public string PageLineWidthLabel => UseRegularLabels
            ? TextProvider.Get(ResX.Settings, "Text.Pages.LineWidth.Label")
            : TextProvider.Get(ResX.Settings, "Text.Pages.LineWidth.Label.Trailing");

        public string SpacesHeader => TextProvider.Get(ResX.Settings, "Text.Spaces.Header");
        public string SpaceFontSizeLabel => UseRegularLabels
            ? TextProvider.Get(ResX.Settings, "Text.Spaces.FontSize.Label")
            : TextProvider.Get(ResX.Settings, "Text.Spaces.FontSize.Label.Trailing");
        public string SpaceLineSpacingLabel => UseRegularLabels
            ? TextProvider.Get(ResX.Settings, "Text.Spaces.LineSpacing.Label")
            : TextProvider.Get(ResX.Settings, "Text.Spaces.LineSpacing.Label.Trailing");

        public string EditingHeader => TextProvider.Get(ResX.Settings, "Text.Editing.Header");
        public string CheckOrthographyLabel => TextProvider.Get(ResX.Settings, "Text.Editing.CheckOrthography.Label");
        public string FetchLinkTitlesLabel => TextProvider.Get(ResX.Settings, "Text.Editing.FetchLinkTitles.Label");
        public string InsertClosingBracketsLabel => TextProvider.Get(ResX.Settings, "Text.Editing.InsertClosingBrackets.Label");

        public List<string> FontNames
        {
            get
            {
                var index = _state.Text.AvaliableFonts.IndexOf(Fonts.SFPro);
                if (index != -1)
                {
                    var fonts = new List<string>(_state.Text.AvaliableFonts);
                    fonts[index] = TextProvider.Get(ResX.Settings, "Text.Fonts.System");
                    return fonts;
                }
                else
                    return _state.Text.AvaliableFonts;
            }
        }
        
        public int SelectedFontIndex
        {
            get => _state.Text.AvaliableFonts.IndexOf(_state.Text.FontName);
            set
            {
                if (value >= 0 && value < _state.Text.AvaliableFonts.Count)
                    _state.Text.FontName = _state.Text.AvaliableFonts[value];
            }
        }

        public bool UseSemibold
        {
            get => _state.Text.UseSemibold;
            set => _state.Text.UseSemibold = value;
        }

        public string PageFontSize
        {
            get => _state.Text.PageFontSize.ToString();
            set
            {
                if (int.TryParse(value, out int size) && size >= 10 && size <= 40)
                    _state.Text.PageFontSize = size;

                RaisePropertyChanged();
            }
        }

        public string PageLineSpacing
        {
            get => _state.Text.PageLineSpacing.ToString("0.0");
            set
            {
                if (float.TryParse(value, out float spacing) && spacing >= 1.0f && spacing <= 2.0f)
                    _state.Text.PageLineSpacing = spacing;

                RaisePropertyChanged();
            }
        }

        public List<string> PageLineWidthNames => new()
        {
            TextProvider.Get(ResX.Settings, "Text.Pages.LineWidth.Compact"),
            TextProvider.Get(ResX.Settings, "Text.Pages.LineWidth.Spacious"),
            TextProvider.Get(ResX.Settings, "Text.Pages.LineWidth.Wide")
        };

        public int SelectedPageLineWidthIndex
        {
            get
            {
                if (_state.Text.PageLineWidth == PageLineWidth.Compact)
                    return 0;
                else if (_state.Text.PageLineWidth == PageLineWidth.Wide)
                    return 2;
                else
                    return 1;
            }
            set
            {
                if (value == 0)
                    _state.Text.PageLineWidth = PageLineWidth.Compact;
                else if (value == 2)
                    _state.Text.PageLineWidth = PageLineWidth.Wide;
                else
                    _state.Text.PageLineWidth = PageLineWidth.Spacious;
            }
        }

        public string SpaceFontSize
        {
            get => _state.Text.SpaceFontSize.ToString();
            set
            {
                if (int.TryParse(value, out int size) && size >= 10 && size <= 40)
                    _state.Text.SpaceFontSize = size;

                RaisePropertyChanged();
            }
        }

        public string SpaceLineSpacing
        {
            get => _state.Text.SpaceLineSpacing.ToString("0.0");
            set
            {
                if (float.TryParse(value, out float spacing) && spacing >= 1.0f && spacing <= 2.0f)
                    _state.Text.SpaceLineSpacing = spacing;

                RaisePropertyChanged();
            }
        }

        public bool CheckOrthography
        {
            get => _state.Text.CheckOrthography;
            set => _state.Text.CheckOrthography = value;
        }

        public bool FetchLinkTitles
        {
            get => _state.Text.FetchLinkTitles;
            set => _state.Text.FetchLinkTitles = value;
        }

        public bool InsertClosingBrackets
        {
            get => _state.Text.InsertClosingBrackets;
            set => _state.Text.InsertClosingBrackets = value;
        }
    }
}
