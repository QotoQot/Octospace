using Core.ViewModels.Settings;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win.Views.Settings
{
#nullable enable
    public abstract class SettingsGeneralViewAbstract : MvxWindowsPage<SettingsGeneralViewModel> { }

    public sealed partial class SettingsGeneralView : SettingsGeneralViewAbstract
    {
        public SettingsGeneralView()
        {
            InitializeComponent();
        }
    }
#nullable restore
}
