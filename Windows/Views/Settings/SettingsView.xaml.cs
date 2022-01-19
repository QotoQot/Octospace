using Core.Logging;
using Core.ViewModels.Settings;
using MvvmCross;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Views;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinUI = Microsoft.UI.Xaml.Controls;

namespace Win.Views.Settings
{
#nullable enable
    public abstract class SettingsViewAbstract : MvxWindowsPage<SettingsViewModel> { }

    public sealed partial class SettingsView : SettingsViewAbstract
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _navigationView.MenuItemsSource = ViewModel.Sections;
            _navigationView.SelectedItem = ViewModel.Sections[0];
        }

        void ShowContent(SettingsSectionViewModel viewModel)
        {
            var viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            var viewType = viewFinder.GetViewType(viewModel.GetType());

            var requestTranslator = Mvx.IoCProvider.Resolve<IMvxWindowsViewModelRequestTranslator>();
            var requestText = requestTranslator.GetRequestTextWithKeyFor(viewModel);

            _contentFrame.Navigate(viewType, requestText);
        }

        void NavigationView_SelectionChanged(WinUI.NavigationView sender, WinUI.NavigationViewSelectionChangedEventArgs args)
        {
            foreach (var item in ViewModel.Sections)
            {
                if (item == args.SelectedItem)
                {
                    ShowContent(item);
                    break;
                }
            }
        }
    }
#nullable restore
}
