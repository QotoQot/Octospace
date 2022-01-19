using Microsoft.Toolkit.Diagnostics;
using MvvmCross;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using System;

namespace iOS.MvvmCross.Views
{
    public static class MvxIosViewExtensions
    {
        public static IMvxIosView CreateViewControllerWithViewModel(this IMvxCanCreateIosView creatingView, IMvxViewModel? viewModel)
        {
            Guard.IsNotNull(viewModel, nameof(viewModel));

            var view = Mvx.IoCProvider.Resolve<IMvxIosViewCreator>().CreateView(viewModel);
            view.ViewModel = viewModel;
            return view;
        }
    }
}
