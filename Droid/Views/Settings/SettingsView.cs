using Core.ViewModels.MainWindow;
using Core.ViewModels.Settings;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using System;
namespace Droid.Views.Settings
{
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(RootViewModel),
                             //FragmentContentId = Resource.Id.____,
                             IsCacheableFragment = true)]
    public class SettingsView : MvxFragment<SettingsViewModel>
    {
        public SettingsView()
        {
        }
    }
}
