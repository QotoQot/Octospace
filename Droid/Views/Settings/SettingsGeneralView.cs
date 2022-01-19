using Core.ViewModels.Settings;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using System;
namespace Droid.Views.Settings
{
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(SettingsViewModel),
                             //FragmentContentId = Resource.Id.____,
                             IsCacheableFragment = true)]
    public class SettingsGeneralView : MvxFragment<SettingsGeneralViewModel>
    {
        public SettingsGeneralView()
        {
        }
    }
}
