using System;

using MvvmCross.ViewModels;

namespace Core.ViewModels.Settings
{
    public class SettingsSectionViewModel : MvxViewModel
    {
        public string Title { get; }

        public SettingsSectionViewModel(string title)
        {
            Title = title;
        }
    }
}