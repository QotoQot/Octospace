using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win.Views.MainWindow
{
#nullable enable
    public class PaneTabView : TabView, IDisposable
    {
        Grid? _tabContainerGrid;

        public PaneTabView()
        {
            TabItemsChanged += TabView_TabItemsChanged;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _tabContainerGrid = (Grid)GetTemplateChild("TabContainerGrid");
        }

        void TabView_TabItemsChanged(TabView sender, IVectorChangedEventArgs args)
        {
            if (_tabContainerGrid == null)
                return;

            if (TabItems.Count > 1)
            {
                _tabContainerGrid.Visibility = Visibility.Visible;
            }
            else
            {
                _tabContainerGrid.Visibility = Visibility.Collapsed;
            }
        }

        public void Dispose()
        {
            TabItemsChanged -= TabView_TabItemsChanged;
        }
    }
#nullable restore
}
