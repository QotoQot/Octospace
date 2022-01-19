using Core.Logging;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win.MvvmCross.Views
{
#nullable enable
    public class MvxWindowsUserControl : UserControl, IMvxView, IDisposable
    {
        public MvxWindowsUserControl()
        {
            Loading += Control_Loading;
            Loaded += Control_Loaded;
            Unloaded += Control_Unloaded;
            DataContextChanged += Control_DataContextChanged;
        }

        IMvxViewModel? _viewModel;
        public IMvxViewModel? ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel == value)
                    return;

                _viewModel = value;

                if (DataContext != value)
                    DataContext = value;

                OnViewModelSet();
            }
        }

        protected virtual void OnViewModelSet() { }

        protected virtual void Control_Loading(FrameworkElement sender, object args)
        {
            ViewModel?.ViewCreated();
            ViewModel?.ViewAppearing();
        }

        protected virtual void Control_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewAppeared();
        }

        protected virtual void Control_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewDestroy();
        }

        void Control_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (_viewModel == null)
                ViewModel = (IMvxViewModel)args.NewValue;
        }

        ~MvxWindowsUserControl() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Loading -= Control_Loading;
                Loaded -= Control_Loaded;
                Unloaded -= Control_Unloaded;
                DataContextChanged -= Control_DataContextChanged;
            }
        }
    }

    public class MvxWindowsUserControl<TViewModel> : MvxWindowsUserControl, IMvxView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel? ViewModel
        {
            get => (TViewModel?)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
#nullable restore
}
