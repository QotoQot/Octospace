using System;
using System.Drawing;
using AppKit;
using Core.Logging;
using CoreGraphics;
using Foundation;

namespace Mac.Views.Content.Documents.Pages
{
    [Register("PageTableView")]
    public class PageTableView : NSTableView
    {
        [Export("initWithCoder:")]
        public PageTableView(NSCoder coder) : base(coder) => Initialize();
        public PageTableView(IntPtr handle) : base(handle) => Initialize();
        public PageTableView(RectangleF frame) : base(frame) => Initialize();

        void Initialize()
        {
            RegisterNib(TextBlockTableCellView.Nib, TextBlockTableCellView.SharedId);
            RegisterForDraggedTypes(new string[] { DataType.Block });
        }

        public override void MouseDown(NSEvent theEvent)
        {
            // TODO: possibly ignore this event completely when
            // not clicked on the handle to prevent problems when dragging non-text blocks

            if (SelectedRowCount == 1)
            {
                var point = ConvertPointFromView(theEvent.LocationInWindow, null);
                var row = GetRow(point);

                if (row == SelectedRow)
                {
                    DeselectRow(row);
                    return;
                }
            }

            base.MouseDown(theEvent);
        }

        public override void ResizeWithOldSuperviewSize(CGSize oldSize)
        {
            base.ResizeWithOldSuperviewSize(oldSize);
            InvalidateIntrinsicContentSize();
        }
    }
}
