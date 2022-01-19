using AppKit;
using Core.Logging;
using CoreGraphics;
using Foundation;
using System;
using System.Drawing;

namespace Mac.Views.Content.Documents.Pages
{
    [Register("TextBlockTextView")]
    public class TextBlockTextView : NSTextView
    {
        bool _isBeingEdited;

        CGSize _lastSuperviewSize;
        nfloat _newHeight;

        [Export("initWithCoder:")]
        public TextBlockTextView(NSCoder coder) : base(coder) => Initialize();
        public TextBlockTextView(IntPtr handle) : base(handle) => Initialize();
        public TextBlockTextView(RectangleF frame) : base(frame) => Initialize();

        public event EventHandler<nfloat>? HeightChanged;
        public event EventHandler<bool>? DidBecomeFirstResponder;
        
        void Initialize()
        {
            TextDidBeginEditing += TextView_TextDidBeginEditing;
            TextDidEndEditing += TextView_TextDidEndEditing;
            TextDidChange += TextView_TextDidChange;

            //TextContainer.WidthTracksTextView = true;
        }

        public override string Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
            }
        }

        public override void ResizeWithOldSuperviewSize(CGSize oldSize)
        {
            //base.ResizeWithOldSuperviewSize(oldSize);

            if (String.IndexOf("Cheese") != -1)
                Dlog.Info("resize with superview " + Superview.Bounds.Size);

            // avoid the calculation on the first pass in a table
            //if (_lastSuperviewSize != CGSize.Empty)
            
            UpdateHeight();
        }

        public void UpdateHeight()
        {
            //if (Bounds.Size == Superview.Bounds.Size)
            //{
            //    // setting the frame updates the size of TextContainer
            //    Frame = new CGRect(CGPoint.Empty, Frame.Size);
            //    return;
            //}

            if (String.IndexOf("Cheese") != -1)
                Dlog.Info("PREV FRAME " + Frame);

            LayoutManager.EnsureLayoutForTextContainer(TextContainer);
            var height = LayoutManager.GetUsedRectForTextContainer(TextContainer).Height;
            Frame = new CGRect(TextBlockTableCellView.LeftPadding,
                               0,
                               Superview.Bounds.Width - TextBlockTableCellView.LeftPadding,
                               height);

            if (String.IndexOf("Cheese") != -1)
                Dlog.Info("NEW FRAME: " + Frame + " / SUPER's BOUNDS: " + Superview.Bounds.Size);

        }

        public override bool BecomeFirstResponder()
        {
            var result = base.BecomeFirstResponder();
            DidBecomeFirstResponder?.Invoke(this, result);
            return result;
        }

        public override void Layout()
        {
            base.Layout();
            HeightChanged?.Invoke(this, Frame.Height);
        }

        //public override CGRect Frame
        //{
        //    get => base.Frame;
        //    set
        //    {
        //        if (value.X != LeftPadding)
        //            return;

        //        base.Frame = value;
        //    }
        //}

        void TextView_TextDidBeginEditing(object sender, EventArgs e)
        {
            _isBeingEdited = true;
        }

        void TextView_TextDidEndEditing(object sender, EventArgs e)
        {
            _isBeingEdited = false;
            //SetSelectedRange(new NSRange(String.Length, 0));
        }

        void TextView_TextDidChange(object sender, EventArgs e)
        {
            UpdateHeight();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TextDidBeginEditing -= TextView_TextDidBeginEditing;
                TextDidEndEditing -= TextView_TextDidEndEditing;
                TextDidChange -= TextView_TextDidChange;
            }
            base.Dispose(disposing);
        }
    }
}
