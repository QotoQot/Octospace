using Core.ViewModels.Settings;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win.Views.Settings
{
#nullable enable
    public abstract class SettingsThemesViewAbstract : MvxWindowsPage<SettingsThemesViewModel> { }

    public sealed partial class SettingsThemesView : SettingsThemesViewAbstract
    {
        public SettingsThemesView()
        {
            InitializeComponent();
        }
    }
#nullable restore
}
