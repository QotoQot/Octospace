using Core.Logging;
using Core.ViewModels.MainWindow;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Platforms.Uap.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Win.MvvmCross.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Win.Views.MainWindow
{
#nullable enable
    public abstract class DetailViewAbstract : MvxWindowsPage<DetailViewModel> { }
    public sealed partial class DetailView : DetailViewAbstract
    {
        public DetailView()
        {
            InitializeComponent();

            Loading += HandleLoading;
            Unloaded += HandleUnloaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        void HandleLoading(FrameworkElement sender, object args)
        {
            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            foreach (var item in ViewModel.Panes)
                AddPane(item);

            //ViewModel.Panes.CollectionChanged += Panes_CollectionChanged;
        }

        void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            //ViewModel.Panes.CollectionChanged -= Panes_CollectionChanged;
        }

        void AddPane(PaneViewModel paneViewModel)
        {
            var pane = new PaneView { ViewModel = paneViewModel };
            _grid.Children.Add(pane);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Loading -= HandleLoading;
                Unloaded -= HandleUnloaded;
            }
        }
    }
#nullable restore
}
