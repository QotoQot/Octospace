using Android.OS;
using Core.ViewModels.MainWindow;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using System;
namespace Droid.Views.MainWindow
{
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(RootViewModel),
                             //FragmentContentId = Resource.Id.____,
                             IsCacheableFragment = true)]
    public class SidebarView : MvxFragment<SidebarViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // https://github.com/bmelnychuk/AndroidTreeView
        }
    }
}
