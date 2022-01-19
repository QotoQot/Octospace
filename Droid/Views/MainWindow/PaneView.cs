using Core.ViewModels.MainWindow;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using System;

namespace Droid.Views.MainWindow
{
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(RootViewModel),
                             //FragmentContentId = Resource.Id.____,
                             IsCacheableFragment = true)]
    public class PaneView : MvxFragment<PaneViewModel>
    {
        public PaneView()
        {
        }
    }
}
