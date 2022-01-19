using System;
using System.Collections.Generic;
using System.ComponentModel;
using AppKit;
using Core.Logging;
using Core.Resources.Localization;
using Core.ViewModels.Content;
using Core.ViewModels.MainWindow;
using Foundation;
using Mac.MvvmCross.Presenters;
using Mac.Platform.Utils;
using MvvmCross;
using MvvmCross.Platforms.Mac.Views;

namespace Mac.Views.MainWindow
{
    public partial class SidebarView : MvxViewController<SidebarViewModel>, INSOutlineViewDataSource, INSOutlineViewDelegate
    {
        [Export("initWithCoder:")]
        public SidebarView(NSCoder coder) : base(coder) => Initialize();
        public SidebarView(IntPtr handle) : base(handle) => Initialize();
        public SidebarView() : base(nameof(SidebarView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Dlog.Info("SIDEBAR view did load");

            // selection still happens if not selected already
            // don't forget to unsub
            //OutlineView.DoubleClick += OutlineView_DoubleClick;

            TransparentOverlay.Layer!.BackgroundColor = new CoreGraphics.CGColor(1f, 1f, 1f, 0.6f);
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public override void ViewWillDisappear()
        {
            base.ViewWillDisappear();
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Items))
                OutlineView.ReloadData();
        }

        // FUTURE: can have two outlines for favorites and tags
        #region Data Source

        // Source binding:
        // https://stackoverflow.com/questions/51131871/mvvmcross-binding-with-nstableview

        [Export("outlineView:numberOfChildrenOfItem:")]
        public nint GetChildrenCount(NSOutlineView outlineView, NSObject item)
        {
            if (item == null)
                return ViewModel.Items.Count;
            else
                return UnwrapSidebarOutlineItem(item).Children.Count;
        }

        [Export("outlineView:child:ofItem:")]
        public NSObject GetChild(NSOutlineView outlineView, nint childIndex, NSObject item)
        {
            int index = (int)childIndex;
            IList<SidebarOutlineItemViewModel> children = ViewModel.Items;

            if (item != null)
                children = UnwrapSidebarOutlineItem(item).Children;

            return new NSWrapper<SidebarOutlineItemViewModel>(children[index]);
        }

        [Export("outlineView:viewForTableColumn:item:")]
        public NSView GetView(NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
        {
            var sidebarOutlineItem = UnwrapSidebarOutlineItem(item);
            var cellId = "ItemCellView";

            if (sidebarOutlineItem.Type == SidebarOutlineItemType.Header)
                cellId = "HeaderCell";

            if (outlineView.MakeView(cellId, this) is not NSTableCellView cellView)
                throw new InvalidOperationException("Can't create an item cell view for an outline");

            // TODO: colors from App Theme + by type
            //cellView.TextField.TextColor =

            cellView.TextField.StringValue = sidebarOutlineItem.Title;


            // TODO: below 11.0
            if (cellView.ImageView != null)
            {
                // TODO: color from the app theme
                cellView.ImageView.ContentTintColor = NSColor.Purple;

                var label = SidebarOutlineItemViewModel.LabelForType(sidebarOutlineItem.Type);
                cellView.ImageView.Image = sidebarOutlineItem.Type switch
                {
                    SidebarOutlineItemType.AllDocuments => NSImage.GetSystemSymbol("doc.text", label), // doc.on.doc
                    SidebarOutlineItemType.Favorites => NSImage.GetSystemSymbol("star", label), // star.square / star.circle
                    SidebarOutlineItemType.Trash => NSImage.GetSystemSymbol("trash", label),
                    SidebarOutlineItemType.Random => NSImage.GetSystemSymbol("die.face.5", label),
                    SidebarOutlineItemType.Daily => NSImage.GetSystemSymbol("calendar", label),
                    SidebarOutlineItemType.Tag => NSImage.GetSystemSymbol("number", label),
                    _ => throw new InvalidOperationException($"No image for sidebar outline item type: {sidebarOutlineItem.Type}")
                };
            }

            return cellView;
        }

        [Export("outlineView:isItemExpandable:")]
        public bool ItemExpandable(NSOutlineView outlineView, NSObject item) => UnwrapSidebarOutlineItem(item).Children.Count > 0;

        [Export("outlineView:isGroupItem:")]
        public bool IsGroupItem(NSOutlineView outlineView, NSObject item) => false;

        [Export("outlineView:shouldSelectItem:")]
        public bool ShouldSelectItem(NSOutlineView outlineView, NSObject item)
        {
            var sidebarOutlineItem = UnwrapSidebarOutlineItem(item);
            return sidebarOutlineItem.Type != SidebarOutlineItemType.Header;
        }

        [Export("outlineViewSelectionDidChange:")]
        public void SelectionDidChange(NSNotification notification)
        {
            var item = OutlineView.ItemAtRow(OutlineView.SelectedRow);
            var sidebarOutlineItem = UnwrapSidebarOutlineItem(item);
            Dlog.Info("Sidebar item selection: " + sidebarOutlineItem.Title);
            ViewModel.ShowSelectedItemCommand.ExecuteAsync(sidebarOutlineItem);
        }

        SidebarOutlineItemViewModel UnwrapSidebarOutlineItem(NSObject item)
        {
            if (item is NSWrapper<SidebarOutlineItemViewModel> wrapper)
            {
                return wrapper.Item;
            }
            else
                throw new ArgumentException("Sidebar recieved unknown object: " + item.GetType());
        }

        [Export("outlineView:rowViewForItem:")]
        public NSTableRowView RowViewForItem(NSOutlineView outlineView, NSObject item)
        {
            return new SidebarOutlineRowView();
        }
        #endregion
    }
}
