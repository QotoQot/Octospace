using System;
using Core.Resources.Localization;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels.Settings
{
    public class SettingsGeneralViewModel : SettingsSectionViewModel
    {
        public SettingsGeneralViewModel()
            : base(TextProvider.Get(ResX.Settings, "General.Tab.Title"))
        {

        }
    }
}
