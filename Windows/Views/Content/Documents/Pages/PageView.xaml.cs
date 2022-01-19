using System;
using System.Collections.Generic;
using Core.Logging;
using Core.ViewModels.Content;
using Core.ViewModels.Content.Documents;
using MvvmCross;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Win.MvvmCross.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;


//<ListView.ItemTemplate>
//    <DataTemplate>
//        <Grid Height="{Binding ActualHeight, ElementName=detailGrid, Converter={StaticResource heightConverter}}" Margin="6">
//            <Grid.ColumnDefinitions>
//                <ColumnDefinition Width="Auto"/>
//                <ColumnDefinition Width="*"/>
//            </Grid.ColumnDefinitions>
//            <Border Background="{StaticResource ListViewItemPlaceholderBackgroundThemeBrush}" Width="10" Height="10">
//                <Image Source="Assets/arrow.png" Stretch="UniformToFill" PointerPressed="Image_PointerPressed"/>
//            </Border>
//            <TextBlock Grid.Column="1" Text="{Binding Title}" TextWrapping="NoWrap"/>
//            <Grid x:Name="detailGrid">
//                <!--Omitted, Show detail information-->
//            </Grid>
//        </Grid>
//    </DataTemplate>
//</ListView.ItemTemplate>


//ItemsRepeater, although it has an object ItemsSource, doesn't actually have built-in support for the following interfaces necessary for working with virtualized data sources:

//- ISupportIncrementalLoading
//- IItemsRangeInfo
//- ISelectionInfo

//https://github.com/djspider117/XamlTemplateSelector

//If WinUI uses ItemsPanels for its items controls like WPF does, then...you should be able to use ItemsStackPanel as part of the other options described.

//As for ListView...I'm not 100% sure, but depending on how the dragging thing is done, you might be able to re-template the items to restrict dragging to a grip on the side, as described?

//and well...using it as the ItemsStackPanel for a ListView as described above would allow you to use it...well, with a data source

namespace Win.Views.Content.Documents.Pages
{
    public abstract class PageViewAbstract : TabContentView<PageViewModel> { }

    public sealed partial class PageView : PageViewAbstract
    {
        MvxSubscriptionToken _pageManipulationRequestedSubscription = null!;

        bool _isDeselecting;

        public PageView() => InitializeComponent();

        protected override void TabContent_Loaded(object sender, RoutedEventArgs e)
        {
            _pageManipulationRequestedSubscription = Mvx.IoCProvider.Resolve<IMvxMessenger>()
                .SubscribeOnMainThread<PageManipulationRequestMessage>(Message_PageManipulationRequested);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            _listView.ItemsSource = ViewModel.Blocks;
        }

        void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var isDeselecting = _isDeselecting;
            _isDeselecting = false;

            if (isDeselecting && e.AddedItems.Count == 1)
                return;

            var indexes = new HashSet<int>();
            foreach (var item in _listView.SelectedRanges)
                for (int i = item.FirstIndex; i <= item.LastIndex; i++)
                    indexes.Add(i);

            ViewModel.UpdateSelection(indexes);
        }

        void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_listView.SelectedItems.Count == 1 && _listView.SelectedItem == e.ClickedItem)
            {
                _listView.SelectedItem = null;
                _isDeselecting = true;
            }
        }

        void Message_PageManipulationRequested(PageManipulationRequestMessage msg)
        {
            //var blockCell = (IBlockTableCellView)msg.Sender;
            //var row = TableView.RowForView((MvxAltTableCellView)blockCell);

            if (msg.PageManipulation == PageManipulation.DeselectAll)
            {
                _listView.SelectedItem = null;
            }
            // TODO: execute move up/down onto the text field if no cell is found
            //else if (msg.PageManipulation == PageManipulation.GoToBlockAbove)
            //{
            //    if (row > 0)
            //    {
            //        var rowAbove = row - 1;
            //        var cellAbove = (IBlockTableCellView)TableView.GetView(0, rowAbove, false);

            //        var focusPoint = new CGPoint(blockCell.ContentFocus.X, cellAbove.ContentSize.Height - 1);
            //        cellAbove.FocusContentAt(focusPoint);
            //    }
            //    else if (blockCell is TextBlockTableCellView textBlockCell)
            //        textBlockCell.MoveCursorToBeginning();
            //}
            //else if (msg.PageManipulation == PageManipulation.GoToBlockBelow)
            //{
            //    if (row < TableView.RowCount - 1)
            //    {
            //        var rowBelow = row + 1;
            //        var cellBelow = (IBlockTableCellView)TableView.GetView(0, rowBelow, false);

            //        var focusPoint = new CGPoint(blockCell.ContentFocus.X, 0);
            //        cellBelow.FocusContentAt(focusPoint);
            //    }
            //    else if (blockCell is TextBlockTableCellView textBlockCell)
            //        textBlockCell.MoveCursorToEnd();
            //}
        }
    }

    internal class BlockTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBlock { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is TextBlockViewModel)
                return TextBlock;
            else
                throw new ArgumentException("Can't find data template for block type: " + item.GetType());
        }
    }

}