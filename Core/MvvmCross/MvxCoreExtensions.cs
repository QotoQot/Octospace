using Core.ViewModels.Content;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
namespace Core.MvvmCross
{
    public static class MvxCoreExtensions
    {
        public static TTargetViewModel LoadViewModel<TTargetViewModel>(this IMvxViewModelLoader viewModelLoader)
            where TTargetViewModel : class, IMvxViewModel
        {
            var request = new MvxViewModelRequest(typeof(TTargetViewModel));
            return (TTargetViewModel)viewModelLoader.LoadViewModel(request, null);
        }

        public static TTargetViewModel LoadViewModel<TTargetViewModel, TParameter>(this IMvxViewModelLoader viewModelLoader, TParameter parameter)
            where TTargetViewModel : class, IMvxViewModel
            where TParameter : notnull
        {
            var request = new MvxViewModelRequest(typeof(TTargetViewModel));
            return (TTargetViewModel)viewModelLoader.LoadViewModel(request, parameter, null);
        }
    }
}
