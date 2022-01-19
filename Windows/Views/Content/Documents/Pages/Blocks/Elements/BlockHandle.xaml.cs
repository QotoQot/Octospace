using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Win.Views.Content.Documents.Pages
{
#nullable enable
    public sealed partial class BlockHandle : UserControl, IDisposable
    {
        bool _isVisible;

        public BlockHandle()
        {
            InitializeComponent();

            Loaded += Control_Loaded;
            Unloaded += Control_Unloaded;
        }

        public void Show(bool isVisible)
        {
            // TODO: content theme colors
            if (isVisible)
                _text.Foreground = new SolidColorBrush(Colors.SlateGray);
            else
                _text.Foreground = new SolidColorBrush(Colors.Transparent);

            _isVisible = isVisible;
        }

        void ShowHighlight(bool isHighlighted)
        {
            // TODO: content theme colors
            if (_isVisible)
            {
                if (isHighlighted)
                    _text.Foreground = new SolidColorBrush(Colors.Black);
                else
                    _text.Foreground = new SolidColorBrush(Colors.SlateGray);
            }
        }

        void Control_Loaded(object sender, RoutedEventArgs e)
        {
            PointerEntered += Block_PointerEntered;
            PointerExited += BaseBlock_PointerExited;
        }

        void Control_Unloaded(object sender, RoutedEventArgs e)
        {
            PointerEntered -= Block_PointerEntered;
            PointerExited -= BaseBlock_PointerExited;
        }

        void Block_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ShowHighlight(true);
        }

        void BaseBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ShowHighlight(false);
        }

        ~BlockHandle() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (disposing)
            {
                Loaded -= Control_Loaded;
                Unloaded -= Control_Unloaded;
            }
        }
    }
#nullable restore
}
