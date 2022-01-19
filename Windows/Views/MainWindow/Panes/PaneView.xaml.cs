using Core.Logging;
using Core.ViewModels.MainWindow;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Win.MvvmCross.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using WinUI = Microsoft.UI.Xaml.Controls;

namespace Win.Views.MainWindow
{
#nullable enable
    public abstract class PaneViewAbstract : MvxWindowsUserControl<PaneViewModel> { }

    public sealed partial class PaneView : PaneViewAbstract
    {
        public PaneView() => InitializeComponent();

        protected override void Control_Loading(FrameworkElement sender, object args)
        {
            base.Control_Loading(sender, args);

            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            foreach (var item in ViewModel.Tabs)
                AddTab(item);

            ViewModel.Tabs.CollectionChanged += Tabs_CollectionChanged;
        }

        protected override void Control_Unloaded(object sender, RoutedEventArgs e)
        {
            base.Control_Unloaded(sender, e);

            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            ViewModel.Tabs.CollectionChanged -= Tabs_CollectionChanged;
        }

        void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                //foreach (SingleTabViewModel item in e.OldItems)
                //{
                //    // TODO: move, close
                //}
            }

            if (e.NewItems != null)
            {
                foreach (SingleTabViewModel item in e.NewItems)
                {
                    AddTab(item);
                }
            }
        }

        void AddTab(SingleTabViewModel singleTabViewModel)
        {
            var tabContent = new SingleTabView();
            tabContent.ViewModel = singleTabViewModel;

            Guard.IsNotNull(singleTabViewModel.Content, nameof(singleTabViewModel.Content));

            var tabItem = new WinUI.TabViewItem
            {
                IconSource = new WinUI.SymbolIconSource { Symbol = Symbol.Placeholder },
                Header = singleTabViewModel.Content.Title,
                Content = tabContent
            };
            _tabView.TabItems.Add(tabItem);

            var tabItem2 = new WinUI.TabViewItem
            {
                IconSource = new WinUI.SymbolIconSource { Symbol = Symbol.Pictures },
                Header = "Some test",
                Content = new TextBox { Text = "123" }
            };
            _tabView.TabItems.Add(tabItem2);

            //var tabItem3 = new WinUI.TabViewItem
            //{
            //    IconSource = new WinUI.SymbolIconSource { Symbol = Symbol.Library },
            //    Header = "Another document"
            //};
            //_tabView.TabItems.Add(tabItem3);
        }
    }
#nullable restore
}
