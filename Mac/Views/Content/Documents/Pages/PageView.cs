using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using Core.ViewModels.Content.Documents;
using MvvmCross.Binding.BindingContext;
using Mac.View.Content;
using System.ComponentModel;
using System.Collections.Specialized;
using Microsoft.Toolkit.Diagnostics;
using CoreGraphics;
using Core.Logging;
using MvvmCross;
using MvvmCross.ViewModels;
using MvvmCross.Plugin.Messenger;
using Mac.Views.Content.Documents.Blocks;
using Mac.MvvmCross.Views;
using System.Threading.Tasks;

namespace Mac.Views.Content.Documents.Pages
{
    public partial class PageView : TabContentView<PageViewModel>, INSTableViewDataSource, INSTableViewDelegate
    {
        MvxSubscriptionToken _pageManipulationRequestedSubscription = null!;

        readonly NSView _toolbarSeparator = new();

        TextBlockTableCellView _textBlockViewPrototype = null!;

        [Export("initWithCoder:")]
        public PageView(NSCoder coder) : base(coder) => Initialize();
        public PageView(IntPtr handle) : base(handle) => Initialize();
        public PageView() : base(nameof(PageView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        readonly NSTextStorage _textStorage = new();
        readonly NSTextContainer _textContainer = new();
        readonly NSLayoutManager _layoutManager = new();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _textStorage.AddLayoutManager(_layoutManager);
            _layoutManager.AddTextContainer(_textContainer);

            // TODO: MvxTableViewSource
            TableView.DataSource = this;
            TableView.Delegate = this;

            _toolbarSeparator.WantsLayer = true;
            _toolbarSeparator.Layer!.BackgroundColor = new CGColor(0.96f, 0.96f, 0.96f, 1);
            _toolbarSeparator.Frame = new CGRect(0, View.Bounds.Height - 1, View.Bounds.Width, 1);
            _toolbarSeparator.AutoresizingMask = NSViewResizingMask.MinXMargin | NSViewResizingMask.MaxXMargin | NSViewResizingMask.WidthSizable | NSViewResizingMask.MinYMargin;
            View.AddSubview(_toolbarSeparator);

            //TableView.EnclosingScrollView.PostsBoundsChangedNotifications
            NSScrollView.Notifications.ObserveDidLiveScroll(TableView.EnclosingScrollView, ScrollView_DidLiveScroll);

            _textBlockViewPrototype = (TextBlockTableCellView)TableView.MakeView(TextBlockTableCellView.SharedId, this);

            var set = this.CreateBindingSet<PageView, PageViewModel>();
            //set.Bind(Text).To(vm => vm.TempText);
            set.Apply();

            // TODO: from content theme
            Guard.IsNotNull(View.Layer, nameof(View.Layer));
            View.Layer.BackgroundColor = CGColor.CreateSrgb(1, 1, 1, 1);

            _pageManipulationRequestedSubscription = Mvx.IoCProvider.Resolve<IMvxMessenger>()
                .SubscribeOnMainThread<PageManipulationRequestMessage>(Message_PageManipulationRequested);
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();

            UpdateToolbarSeparatorVisibility();

            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            ViewModel.Blocks.CollectionChanged += Blocks_CollectionChanged;
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();

            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            ViewModel.Blocks.CollectionChanged -= Blocks_CollectionChanged;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        void Blocks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Task.Delay(1000).ContinueWith(task =>
            //{
                TableView.ReloadData();
            //}, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void ScrollView_DidLiveScroll(object sender, NSNotificationEventArgs e) => UpdateToolbarSeparatorVisibility();

        void UpdateToolbarSeparatorVisibility()
        {
            var clipViewY = TableView.EnclosingScrollView.ContentView.Bounds.Y;
            _toolbarSeparator.Hidden = clipViewY < 10;
        }

        void Message_PageManipulationRequested(PageManipulationRequestMessage msg)
        {
            var blockCell = (IBlockTableCellView)msg.Sender;
            var row = TableView.RowForView((MvxAltTableCellView)blockCell);

            if (msg.PageManipulation == PageManipulation.DeselectAll)
            {
                TableView.DeselectAll(this);
            }
            // TODO: execute move up/down onto the text field if no cell is found
            else if (msg.PageManipulation == PageManipulation.GoToBlockAbove)
            {
                if (row > 0)
                {
                    var rowAbove = row - 1;
                    var cellAbove = (IBlockTableCellView)TableView.GetView(0, rowAbove, false);

                    var focusPoint = new CGPoint(blockCell.ContentFocus.X, cellAbove.ContentSize.Height-1);
                    cellAbove.FocusContentAt(focusPoint);
                }
                else if (blockCell is TextBlockTableCellView textBlockCell)
                    textBlockCell.MoveCursorToBeginning();
            }
            else if (msg.PageManipulation == PageManipulation.GoToBlockBelow)
            {
                if (row < TableView.RowCount - 1)
                {
                    var rowBelow = row + 1;
                    var cellBelow = (IBlockTableCellView)TableView.GetView(0, rowBelow, false);

                    var focusPoint = new CGPoint(blockCell.ContentFocus.X, 0);
                    cellBelow.FocusContentAt(focusPoint);
                }
                else if (blockCell is TextBlockTableCellView textBlockCell)
                    textBlockCell.MoveCursorToEnd();
            }
        }


        #region Table Data Source - Rows

        [Export("tableView:heightOfRow:")]
        public nfloat GetRowHeight(NSTableView tableView, nint row)
        {
            var textViewWidth = tableView.TableColumns()[0].Width - TextBlockTableCellView.LeftPadding;
            //if (_textBlockViewPrototype.Bounds.Width != columnWidth)
            //_textBlockViewPrototype.Frame = new CGRect(0, 0, columnWidth, 9999);

            //_textBlockViewPrototype.TempSetText(((TextBlockViewModel)ViewModel.Blocks[(int)row]).Text);

            //var height = 100;
            //var height = new Random().Next(20, 100);

            _textContainer.ContainerSize = new CGSize(textViewWidth, 9999);
            _textStorage.InvalidateAttributes(new NSRange(0, _textStorage.Length));
            _textStorage.SetString(new NSAttributedString(((TextBlockViewModel)ViewModel.Blocks[(int)row]).Text));

            _layoutManager.EnsureLayoutForTextContainer(_textContainer);
            var height = _layoutManager.GetUsedRectForTextContainer(_textContainer).Height;

            if (row == 1)
            {
                Dlog.Info(height);
                //Dlog.Info(_textBlockViewPrototype.ContentSize.Height);
            }

            return height;
            //return _textBlockViewPrototype.ContentSize.Height;
        }

        void View_TestChanged(object sender, nfloat e)
        {
            var row = TableView.RowForView((NSView)sender);
            if (row != -1)
            {
                Guard.IsNotNull(ViewModel, nameof(ViewModel));
                ((TextBlockViewModel)ViewModel.Blocks[(int)row]).Text = ((TextBlockTableCellView)sender).TempText;

                //NSAnimationContext.BeginGrouping();
                //NSAnimationContext.CurrentContext.Duration = 0;
                //TableView.NoteHeightOfRowsWithIndexesChanged(new NSIndexSet(row));
                //NSAnimationContext.EndGrouping();
            }
        }

        [Export("tableView:viewForTableColumn:row:")]
        public NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var view = (TextBlockTableCellView)TableView.MakeView(TextBlockTableCellView.SharedId, this);

            Guard.IsNotNull(ViewModel, nameof(ViewModel));
            if (((TextBlockViewModel)ViewModel.Blocks[(int)row]).Text.IndexOf("Cheese") != -1)
                Dlog.Info("Table " + TableView.Bounds.Size);

            // TODO: inside view model?
            view.ViewModel = (TextBlockViewModel)ViewModel.Blocks[(int)row];
            view.TempSetText(((TextBlockViewModel)ViewModel.Blocks[(int)row]).Text);

            view.TestChanged += View_TestChanged;

            return view;
        }

        [Export("tableView:didAddRowView:forRow:")]
        public void DidAddRowView(NSTableView tableView, NSTableRowView rowView, nint row)
        {
            //var view = (TextBlockTableCellView)rowView.ViewAtColumn(0);
            //view.TempSetText(((TextBlockViewModel)ViewModel.Blocks[(int)row]).Text);
        }

        //[Export("tableView:didRemoveRowView:forRow:")]
        //public void DidRemoveRowView(NSTableView tableView, NSTableRowView rowView, nint row) => throw new System.NotImplementedException();

        [Export("numberOfRowsInTableView:")]
        public nint GetRowCount(NSTableView tableView) => ViewModel != null ? ViewModel.Blocks.Count : 0;

        [Export("tableView:isGroupRow:")]
        public bool IsGroupRow(NSTableView tableView, nint row) => false;

        //[Export("tableView:objectValueForTableColumn:row:")]
        //public NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, nint row)
        //{
        //    throw new System.NotImplementedException();
        //}
        #endregion


        #region Table Delegate - Columns

        [Export("tableViewColumnDidResize:")]
        public void ColumnDidResize(NSNotification notification)
        {
            var allIndexes = NSIndexSet.FromNSRange(new NSRange(0, TableView.RowCount));
            TableView.NoteHeightOfRowsWithIndexesChanged(allIndexes);
        }

        [Export("tableView:shouldSelectTableColumn:")]
        public bool ShouldSelectTableColumn(NSTableView tableView, NSTableColumn tableColumn) => false;

        [Export("tableView:shouldReorderColumn:toColumn:")]
        public bool ShouldReorder(NSTableView tableView, nint columnIndex, nint newColumnIndex) => false;
        #endregion


        #region Table Delegate - Selection

        //[Export("selectionShouldChangeInTableView:")]
        //public bool SelectionShouldChange(NSTableView tableView) => true;

        //[Export("tableView:shouldSelectRow:")]
        //public bool ShouldSelectRow(NSTableView tableView, nint row) => true;

        //[Export("tableViewSelectionIsChanging:")]
        //public void SelectionIsChanging(NSNotification notification) => Dlog.Info("changing");

        [Export("tableViewSelectionDidChange:")]
        public void SelectionDidChange(NSNotification notification)
        {
            var indexes = new HashSet<int>();
            foreach (var item in TableView.SelectedRows)
                indexes.Add((int)item);

            ViewModel?.UpdateSelection(indexes);
        }

        //[Export("tableView:selectionIndexesForProposedSelection:")]
        //public NSIndexSet GetSelectionIndexes(NSTableView tableView, NSIndexSet proposedSelectionIndexes) => throw new System.NotImplementedException();
        #endregion


        #region Table Data Source - Dragging
        //!missing-protocol-member! NSDraggingDestination::updateDraggingItemsForDrag: not found
        //!missing-protocol-member! NSDraggingSource::draggingSession:endedAtPoint:operation: not found
        //!missing-protocol-member! NSDraggingSource::draggingSession:movedToPoint: not found
        //!missing-protocol-member! NSDraggingSource::draggingSession:sourceOperationMaskForDraggingContext: not found
        //!missing-protocol-member! NSDraggingSource::draggingSession:willBeginAtPoint: not found
        //!missing-protocol-member! NSDraggingSource::ignoreModifierKeysForDraggingSession: not found

        [Export("tableView:pasteboardWriterForRow:")]
        public INSPasteboardWriting GetPasteboardWriterForRow(NSTableView tableView, nint row)
        {
            var pasteboardItem = new NSPasteboardItem();
            pasteboardItem.SetStringForType(row.ToString(), DataType.Block);
            return pasteboardItem;
        }

        [Export("tableView:validateDrop:proposedRow:proposedDropOperation:")]
        public NSDragOperation ValidateDrop(NSTableView tableView, NSDraggingInfo info, nint row, NSTableViewDropOperation dropOperation)
        {
            if (dropOperation != NSTableViewDropOperation.Above)
                return NSDragOperation.None;

            return NSDragOperation.Move;
        }


        [Export("tableView:acceptDrop:row:dropOperation:")]
        public bool AcceptDrop(NSTableView tableView, NSDraggingInfo info, nint row, NSTableViewDropOperation dropOperation)
        {
            // TODO: crashing on table reload
            return false;

            if (row < 0 || dropOperation != NSTableViewDropOperation.Above)
                return false;

            var pasteboardItems = info.DraggingPasteboard.PasteboardItems;
            if (pasteboardItems.Length == 0)
                return false;

            if (info.DraggingSource == tableView)
            {
                var selectedIndexes = new HashSet<int>();

                foreach (var item in pasteboardItems)
                {
                    if (int.TryParse(item.GetStringForType(DataType.Block), out int oldIndex))
                        selectedIndexes.Add(oldIndex);
                    else
                        throw new ArgumentException("Table received a drop with incorrect row strings");
                }

                ViewModel.RearrangeBlocks(selectedIndexes, (int)row);
            }
            else
            {
                // TODO: cut & insert blocks from one document to another
            }

            //var oldIndexOffset = 0;
            //var newIndexOffset = 0;
            //var newIndex = row;

            //tableView.BeginUpdates();

            //foreach (var oldIndex in selectedIndexes)
            //{
            //    if (oldIndex < newIndex)
            //    {
            //        tableView.MoveRow(oldIndex + oldIndexOffset, newIndex - 1);
            //        oldIndexOffset--;
            //    }
            //    else
            //    {
            //        tableView.MoveRow(oldIndex, newIndex + newIndexOffset);
            //        newIndexOffset++;
            //    }
            //}

            //tableView.EndUpdates();

            // not needed, observable
            //tableView.ReloadData();

            return true;
        }

        //[Export("tableView:draggingSession:willBeginAtPoint:forRowIndexes:")]
        //public void DraggingSessionWillBegin(NSTableView tableView, NSDraggingSession draggingSession, CGPoint willBeginAtScreenPoint, NSIndexSet rowIndexes) => throw new System.NotImplementedException();

        //[Export("tableView:updateDraggingItemsForDrag:")]
        //public void UpdateDraggingItems(NSTableView tableView, NSDraggingInfo draggingInfo) => throw new System.NotImplementedException();

        //[Export("tableView:draggingSession:endedAtPoint:operation:")]
        //public void DraggingSessionEnded(NSTableView tableView, NSDraggingSession draggingSession, CGPoint endedAtScreenPoint, NSDragOperation operation) => throw new System.NotImplementedException();
        #endregion
    }
}
