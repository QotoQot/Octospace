using AndroidX.Fragment.App;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvvmCross;
using MvvmCross.Platforms.Android.Views.Fragments;
using Microsoft.Toolkit.Diagnostics;

namespace Droid.MvvmCross.Views
{
    public static class MvxAndroidViewExtensions
    {
        public static Fragment CreateFragmentWithViewModel(this FragmentManager fragmentManager, IMvxViewModel? viewModel)
        {
            Guard.IsNotNull(viewModel, nameof(viewModel));

            var fragmentType = Mvx.IoCProvider.Resolve<IMvxViewsContainer>().GetViewType(viewModel.GetType());
            var fragmentClass = Class.FromType(fragmentType);

            var fragmentView = (IMvxFragmentView)fragmentManager.FragmentFactory.Instantiate(fragmentClass.ClassLoader, fragmentClass.Name);
            fragmentView.ViewModel = viewModel;

            var fragment = fragmentView.ToFragment();
            Guard.IsNotNull(fragment, fragmentType.FragmentJavaName());
            return fragment;
        }
    }
}
