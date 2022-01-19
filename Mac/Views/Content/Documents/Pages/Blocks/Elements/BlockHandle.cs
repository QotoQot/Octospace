using System;
using System.Drawing;
using AppKit;
using Core.Logging;
using CoreGraphics;
using Foundation;
using Mac.Platform.Utils;

namespace Mac.Views.Content.Documents.Pages
{
    [Register("BlockHandle")]
    public partial class BlockHandle : NSImageView
    {
        bool _isBeingDragged = false;
        NSImage _regular = null!;
        NSImage _highlighted = null!;

        [Export("initWithCoder:")]
        public BlockHandle(NSCoder coder) : base(coder) => Initialize();
        public BlockHandle(IntPtr handle) : base(handle) => Initialize();
        public BlockHandle(RectangleF frame) : base(frame) => Initialize();

        void Initialize()
        {
            NSTrackingAreaOptions options = NSTrackingAreaOptions.ActiveInKeyWindow |
                                            NSTrackingAreaOptions.MouseMoved |
                                            NSTrackingAreaOptions.MouseEnteredAndExited |
                                            NSTrackingAreaOptions.InVisibleRect;
            AddTrackingArea(new NSTrackingArea(Bounds, options, this, null));

            // TODO: theme colors
            _regular = CreateImage(NSColor.LightGray);
            _highlighted = CreateImage(NSColor.Black);

            ShowHighlight(false);
        }

        void ShowHighlight(bool isHighlighted) => Image = isHighlighted ? _highlighted : _regular;

        public override void ResizeWithOldSuperviewSize(CGSize oldSize)
        {
            base.ResizeWithOldSuperviewSize(oldSize);
            this.AlignTopToParentTop(4);
            this.AlignLeadingToParentLeading();
        }

        void DrawDot(float x, float y)
        {
            var dot = new CGRect(x, y, 2, 2);
            NSBezierPath.FromOvalInRect(dot).Fill();
        }

        public override void MouseDown(NSEvent theEvent)
        {
            base.MouseDown(theEvent);
            //_isBeingDragged = true;
        }

        public override void MouseUp(NSEvent theEvent)
        {
            base.MouseUp(theEvent);
            //_isBeingDragged = false;
        }

        public override void MouseEntered(NSEvent theEvent)
        {
            base.MouseEntered(theEvent);
            ShowHighlight(true);
        }

        public override void MouseExited(NSEvent theEvent)
        {
            base.MouseExited(theEvent);
            ShowHighlight(false);
        }

        public override void MouseDragged(NSEvent theEvent)
        {
            base.MouseDragged(theEvent);
        }

        NSImage CreateImage(NSColor color)
        {
            var image = new NSImage(Bounds.Size);
            image.LockFocus();
            color.Set();

            DrawDot(0, 0);
            DrawDot(0, 4);
            DrawDot(0, 8);

            DrawDot(4, 0);
            DrawDot(4, 4);
            DrawDot(4, 8);

            image.UnlockFocus();
            return image;
        }
    }
}
