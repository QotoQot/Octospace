using Core.Model.State;
using Core.Resources.Localization;
using Core.Resources.Themes;
using Core.Resources.Themes.App;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;

namespace Core.ViewModels.Settings
{
    public class SettingsThemesViewModel : SettingsSectionViewModel
    {
        readonly IState _state;

        public SettingsThemesViewModel(IState state)
            : base(TextProvider.Get(ResX.Settings, "Themes.Tab.Title"))
        {
            _state = state;
        }

        public string ColorModeLabel => TextProvider.Get(ResX.Settings, "Themes.ColorMode.Label");

        public List<string> ColorModeNames => new()
        {
            TextProvider.Get(ResX.Settings, "Themes.ColorMode.System"),
            TextProvider.Get(ResX.Settings, "Themes.ColorMode.Light"),
            TextProvider.Get(ResX.Settings, "Themes.ColorMode.Dark")
        };

        public int SelectedColorModeIndex
        {
            get => (int)Themes.App.CurrentModeSetting;
            set => Themes.App.CurrentModeSetting = (AppTheme.ModeSetting)value;
        }

        public string ContentThemesFollowAppThemesLabel => TextProvider.Get(ResX.Settings, "Themes.ContentThemesFollowAppThemes.Label");

        public bool ContentThemesFollowAppThemes
        {
            get => Themes.Content.ContentThemesFollowAppThemes;
            set => Themes.Content.ContentThemesFollowAppThemes = value;
        }
    }
}
