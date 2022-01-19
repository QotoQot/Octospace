using Core.Logging;
using Core.Resources.Localization;
using Core.ViewModels.MainWindow;
using MvvmCross;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using Win.MvvmCross.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinUI = Microsoft.UI.Xaml.Controls;

namespace Win.Views.MainWindow
{
#nullable enable
    public abstract class SidebarViewAbstract : MvxWindowsUserControl<SidebarViewModel> { }

    public sealed partial class SidebarView : SidebarViewAbstract
    {
        public SidebarView() => InitializeComponent();

        public WinUI.NavigationView NavigationView => _navigationView;
        public Frame ContentFrame => _contentFrame;

        public void ShowContent(IMvxViewModel viewModel)
        {
            var viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            var viewType = viewFinder.GetViewType(viewModel.GetType());

            var requestTranslator = Mvx.IoCProvider.Resolve<IMvxWindowsViewModelRequestTranslator>();
            var requestText = requestTranslator.GetRequestTextWithKeyFor(viewModel);

            _contentFrame.Navigate(viewType, requestText);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _navigationView.MenuItemsSource = ViewModel!.Items;

            var newDocumentBtn = SidebarOutlineItemViewModel.CreateButton(SidebarOutlineItemType.NewDocument);
            _navigationView.FooterMenuItemsSource = new List<SidebarOutlineItemViewModel>{ newDocumentBtn };
        }
    }

    internal class SidebarItemsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Header { get; set; } = null!;
        public DataTemplate Tag { get; set; } = null!;
        public DataTemplate AllDocuments { get; set; } = null!;
        public DataTemplate Favorites { get; set; } = null!;
        public DataTemplate Trash { get; set; } = null!;
        public DataTemplate Random { get; set; } = null!;
        public DataTemplate Daily { get; set; } = null!;
        public DataTemplate NewDocument { get; set; } = null!;

        protected override DataTemplate SelectTemplateCore(object item)
        {
            var sidebarItem = (SidebarOutlineItemViewModel)item;

            return sidebarItem.Type switch
            {
                SidebarOutlineItemType.Header => Header,
                SidebarOutlineItemType.Tag => Tag,
                SidebarOutlineItemType.AllDocuments => AllDocuments,
                SidebarOutlineItemType.Favorites => Favorites,
                SidebarOutlineItemType.Trash => Trash,
                SidebarOutlineItemType.Random => Random,
                SidebarOutlineItemType.Daily => Daily,
                SidebarOutlineItemType.NewDocument => NewDocument,
                _ => throw new ArgumentException("Can't find data template for sidebar item type: " + sidebarItem.Type)
            };
        }
    }
#nullable restore
}
