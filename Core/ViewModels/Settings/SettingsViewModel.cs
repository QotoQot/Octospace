using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Core.Model.State;
using MvvmCross.ViewModels;

namespace Core.ViewModels.Settings
{
    public class SettingsViewModel : MvxViewModel
    {
        IState _state;

        public SettingsViewModel(IState state)
        {
            _state = state;
        }

        protected List<SettingsSectionViewModel> _sections = new();
        public ReadOnlyCollection<SettingsSectionViewModel> Sections => _sections.AsReadOnly();

        public override async Task Initialize()
        {
            await base.Initialize();

            //_sections.Add(new SettingsGeneralViewModel(_state));
            _sections.Add(new SettingsTextViewModel(_state));
            _sections.Add(new SettingsThemesViewModel(_state));
        }
    }
}
