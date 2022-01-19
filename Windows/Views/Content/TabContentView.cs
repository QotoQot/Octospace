using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.MvvmCross.Views;
using Windows.UI.Xaml;

namespace Win.Views.Content
{
    public abstract class TabContentView<TViewModel> : MvxWindowsPage<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public TabContentView()
        {
            Loading += TabContent_Loading;
            Loaded += TabContent_Loaded;
            Unloaded += TabContent_Unloaded;
        }

        protected virtual void TabContent_Loading(FrameworkElement sender, object args) { }

        protected virtual void TabContent_Loaded(object sender, RoutedEventArgs e) { }

        protected virtual void TabContent_Unloaded(object sender, RoutedEventArgs e) { }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Loading -= TabContent_Loading;
                Loaded -= TabContent_Loaded;
                Unloaded -= TabContent_Unloaded;
            }
        }
    }
}
