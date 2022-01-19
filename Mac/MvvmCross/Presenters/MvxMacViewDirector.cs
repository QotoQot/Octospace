using Mac.MvvmCross.Presenters.Attributes;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Navigation;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Reflection;

namespace Mac.MvvmCross.Presenters
{
    public class MvxMacViewDirector : IMvxMacViewDirector
    {
        readonly IMvxViewsContainer _viewContainer;
        readonly IMvxAltMacViewPresenter _viewPresenter;
        readonly IMvxNavigationService _navigationService;

        public MvxMacViewDirector()
        {
            _viewContainer = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            _viewPresenter = (IMvxAltMacViewPresenter)Mvx.IoCProvider.Resolve<IMvxViewPresenter>();
            _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        public void ShowView<TViewModel>() where TViewModel: MvxViewModel
        {
            Type? viewType = _viewContainer.GetViewType(typeof(TViewModel));
            Guard.IsNotNull(viewType, nameof(viewType));

            if (viewType.GetCustomAttribute<UniqueWindowPresentationAttribute>() != null)
            {
                if (_viewPresenter.ShowPreviouslyOpenedWindow<TViewModel>() == false)
                    _navigationService.Navigate<TViewModel>();
            }
            else if (viewType.GetCustomAttribute<MvxBasePresentationAttribute>() == null)
            {
                throw new MvxException($"No presentation attribute set for the view {viewType} ({typeof(TViewModel)})");
            }
            else
                _navigationService.Navigate<TViewModel>();
        }
    }
}
