using Core.Logging;
using Core.ViewModels.Content.Documents;
using System;
using System.Collections.Generic;
using Win.MvvmCross.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win.Views.Content.Documents.Pages
{
#nullable enable
    public abstract class TextBlockListViewItemAbstract : BaseBlockListViewItem<TextBlockViewModel> { }

    public sealed partial class TextBlockListViewItem : TextBlockListViewItemAbstract
    {
        public static readonly int LeftPadding = 20;

        public TextBlockListViewItem() => InitializeComponent();

        public override Control ContentView => _textBox;

        protected override BlockHandle Handle => _handle;
        protected override Border Overlay => _overlay;

        void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ViewModel?.Messenger.Publish(new PageManipulationRequestMessage(this, PageManipulation.DeselectAll));
        }

        void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }
    }
#nullable restore
}
