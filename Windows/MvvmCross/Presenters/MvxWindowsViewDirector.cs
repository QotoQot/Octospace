using Core.ViewModels.MainWindow;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.Navigation;
using MvvmCross.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Win.MvvmCross.Presenters
{
    class MvxWindowsViewDirector : IMvxWindowsViewDirector
    {
        readonly IMvxViewsContainer _viewContainer;
        //readonly IWindowsViewPresenter _viewPresenter;
        readonly IMvxNavigationService _navigationService;

        public MvxWindowsViewDirector()
        {
            _viewContainer = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            //_viewPresenter = Mvx.IoCProvider.Resolve<IMvxViewPresenter>() as IWindowsViewPresenter;
            _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        }

        public void ShowView<TViewModel>() where TViewModel : MvxViewModel
        {
            Type viewType = _viewContainer.GetViewType(typeof(TViewModel));

            if (viewType.GetCustomAttribute<MvxBasePresentationAttribute>() == null)
            {
                throw new MvxException($"No presentation attribute set for the view {viewType} ({typeof(TViewModel)})");
            }
            else
                _navigationService.Navigate<TViewModel>();
        }
    }
}
