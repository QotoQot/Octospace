using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Logging;
using Core.Model.Services;
using Core.MvvmCross;
using Core.ViewModels.Settings;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Core.ViewModels.MainWindow
{
    public class RootViewModel : MvxViewModel
    {
        readonly IMvxViewModelLoader _viewModelLoader;

        public RootViewModel(IMvxViewModelLoader viewModelLoader)
        {
            Dlog.Info("ROOT VM constructor");
            _viewModelLoader = viewModelLoader;
        }

        public SidebarViewModel Sidebar { get; private set; } = null!;
        public DetailViewModel Detail { get; private set; } = null!;

        public async override Task Initialize()
        {
            await base.Initialize();
            //>>> NO AWAIT HERE <<<//

            Dlog.Info("ROOT VM init");
            Sidebar = _viewModelLoader.LoadViewModel<SidebarViewModel>();
            Detail = _viewModelLoader.LoadViewModel<DetailViewModel>();

            // TODO: bind "Loading…" to InitializeTask
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
            Dlog.Info("VM: ROOT VIEW WAS CREATED");
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            Dlog.Info("VM: ROOT VIEW DID APPEAR");
        }

        // Windows-only because of its settings-in-details UX
        public SettingsViewModel CreateSettings()
        {
            return _viewModelLoader.LoadViewModel<SettingsViewModel>();
        }
    }
}
