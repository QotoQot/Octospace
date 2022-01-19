using System;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.ViewModels;

namespace Mac.MvvmCross.Views
{
    public static class MvxMacViewExtensions
    {
        public static IMvxMacView CreateViewControllerWithViewModel(this IMvxCanCreateMacView creatingView, IMvxViewModel? viewModel)
        {
            Guard.IsNotNull(viewModel, nameof(viewModel));
            var view = Mvx.IoCProvider.Resolve<IMvxMacViewCreator>().CreateView(viewModel);
            view.ViewModel = viewModel;
            return view;
        }

        // probably do not use
        //public static IMvxMacView CreateViewControllerFor(this IMvxCanCreateMacView creatingView, Type viewModelType)
        //{
        //    var request = new MvxViewModelRequest(viewModelType);
        //    var viewModel = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
        //    var view = Mvx.IoCProvider.Resolve<IMvxMacViewCreator>().CreateView(request);

        //    view.ViewModel = viewModel;
        //    return view;
        //}

        //public static IMvxMacView CreateViewControllerFor<TTargetViewModel>(
        //    this IMvxCanCreateMacView view,
        //    MvxViewModelRequest request)
        //    where TTargetViewModel : class, IMvxViewModel
        //{
        //    return Mvx.IoCProvider.Resolve<IMvxMacViewCreator>().CreateView(request);
        //}
    }
}
