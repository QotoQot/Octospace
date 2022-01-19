using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

using WinUI = Microsoft.UI.Xaml.Controls;

namespace Win.Views.MainWindow
{
#nullable enable
    public class SidebarNavigationView : WinUI.NavigationView, IDisposable
    {
        public SidebarNavigationView()
        {
            Loaded += NavigationView_Loaded;
        }

        void NavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            //var rootGrid = (Grid)VisualTreeHelper.GetChild(this, 0);
            //var contentGrid = (Grid)VisualTreeHelper.GetChild(rootGrid, 1);
            //var splitView = (SplitView)VisualTreeHelper.GetChild(contentGrid, 1);
            //var grid = (Grid)splitView.Pane;

            //grid.ColumnDefinitions.Add(new ColumnDefinition
            //{
            //    Width = new GridLength(0, GridUnitType.Auto)
            //});

            //grid.ColumnDefinitions.Add(new ColumnDefinition
            //{
            //    Width = new GridLength(20, GridUnitType.Pixel)
            //});


        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //_tabContainerGrid = (Grid)GetTemplateChild("TabContainerGrid");

            //VisualTreeHelper.GetChild()
        }

        public void Dispose()
        {
            Loaded -= NavigationView_Loaded;
        }
    }
#nullable restore
}
