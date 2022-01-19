using Core.ViewModels.MainWindow;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Win.MvvmCross.Views;
using Win.Views.Content;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win.Views.MainWindow
{
#nullable enable
    public abstract class SingleTabViewAbstract : MvxWindowsUserControl<SingleTabViewModel> { }

    public sealed partial class SingleTabView : SingleTabViewAbstract
    {
        public SingleTabView() => InitializeComponent();

        protected override void Control_Loading(FrameworkElement sender, object args)
        {
            UpdateContent();
            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        protected override void Control_Unloaded(object sender, RoutedEventArgs e)
        {
            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Content))
            {
                UpdateContent();
            }
        }

        void UpdateContent()
        {
            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            if (ViewModel.Content == null)
                return;

            var viewFinder = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            var contentViewType = viewFinder.GetViewType(ViewModel.Content.GetType());

            var requestTranslator = Mvx.IoCProvider.Resolve<IMvxWindowsViewModelRequestTranslator>();
            var requestText = requestTranslator.GetRequestTextWithKeyFor(ViewModel.Content);

            _contentFrame.Navigate(contentViewType, requestText);
        }
    }
#nullable restore
}
