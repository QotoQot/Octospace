using Core.Logging;
using Core.Model.State;
using Core.ViewModels.Content.Documents;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using MvvmCross.WeakSubscription;
using System;
using System.ComponentModel;
using System.Numerics;
using Win.MvvmCross.Views;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Win.Views.Content.Documents.Pages
{
#nullable enable
    public interface IBlockListViewItem
    {
        Vector2 ContentSize { get; }
        Vector2 ContentFocus { get; }

        //void FocusContentAt(Vector2 point);
    }

    public abstract class BaseBlockListViewItem<TViewModel> : MvxWindowsUserControl<TViewModel>, IBlockListViewItem
        where TViewModel : class, IMvxViewModel, IBlockViewModel
    {
        IDisposable? _viewModelPropertyChangedSubscription;
        MvxSubscriptionToken _textSettingsPageLineWidthChangedSubscription = null!;

        protected abstract BlockHandle Handle { get; }
        protected abstract Border Overlay { get; }

        public abstract Control ContentView { get; }
        public virtual Vector2 ContentSize => ContentView != null ? ContentView.ActualSize : Vector2.Zero;
        public virtual Vector2 ContentFocus => Vector2.Zero;

        //public virtual Vector2 ContentFocus => Vector2.Zero;
        //TextBlockListViewItem

        //public abstract void FocusContentAt(CGPoint point);

        protected override void OnViewModelSet()
        {
            UpdateMaxWidth();

            if (ViewModel is IMvxNotifyPropertyChanged vm)
                _viewModelPropertyChangedSubscription = vm.WeakSubscribe(ViewModel_PropertyChanged);
        }

        protected override void Control_Loaded(object sender, RoutedEventArgs e)
        {
            base.Control_Loaded(sender, e);

            PointerEntered += Block_PointerEntered;
            PointerExited += BaseBlock_PointerExited;

            _textSettingsPageLineWidthChangedSubscription = Mvx.IoCProvider.Resolve<IMvxMessenger>()
                .SubscribeOnMainThread<TextSettingsPageLineWidthChangedMessage>(Message_TextSettingsPageLineWidthChanged);

            UpdateSelection();
        }

        protected override void Control_Unloaded(object sender, RoutedEventArgs e)
        {
            base.Control_Unloaded(sender, e);

            PointerEntered -= Block_PointerEntered;
            PointerExited -= BaseBlock_PointerExited;
        }

        void Block_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (ViewModel != null && ViewModel.IsSelected == false)
                Handle.Show(true);
        }

        void BaseBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (ViewModel == null || ViewModel.IsSelected == false)
                Handle.Show(false);
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.IsSelected))
                UpdateSelection();
        }

        void Message_TextSettingsPageLineWidthChanged(TextSettingsPageLineWidthChangedMessage msg) => UpdateMaxWidth();

        void UpdateMaxWidth()
        {
            if (ViewModel != null)
                MaxWidth = (int)ViewModel.State.Text.PageLineWidth + TextBlockListViewItem.LeftPadding;
        }

        void UpdateSelection()
        {
            var isSelected = ViewModel != null && ViewModel.IsSelected;
            Handle.Show(isSelected);

            // TODO: content theme selection color
            if (isSelected)
                Overlay.Background = new SolidColorBrush(Colors.LightSlateGray);
            else
                Overlay.Background = new SolidColorBrush(Colors.Transparent);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _viewModelPropertyChangedSubscription?.Dispose();
                _viewModelPropertyChangedSubscription = null;
            }
        }
    }
#nullable restore
}
