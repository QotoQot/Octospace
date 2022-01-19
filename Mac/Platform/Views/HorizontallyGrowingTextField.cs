using AppKit;
using CoreGraphics;
using Foundation;
using System;
using System.Drawing;

namespace Mac.Platform.Views
{
    // https://christiantietze.de/posts/2020/02/auto-growing-nstextfield/

    [Register("HorizontallyGrowingTextField")]
    public class HorizontallyGrowingTextField : NSTextField
    {
        bool _isBeingEdited;
        CGSize? _lastSize;

        [Export("initWithCoder:")]
        public HorizontallyGrowingTextField(NSCoder coder) : base(coder) => Initialize();
        public HorizontallyGrowingTextField(IntPtr handle) : base(handle) => Initialize();
        public HorizontallyGrowingTextField(RectangleF frame) : base(frame) => Initialize();
        void Initialize() { }

        public override string StringValue
        {
            get => base.StringValue;
            set
            {
                base.StringValue = value;

                if (_isBeingEdited)
                    _lastSize = SizeForProgrammaticText(StringValue);
            }
        }

        public override CGSize IntrinsicContentSize
        {
            get
            {
                CGSize minSize()
                {
                    var size = base.IntrinsicContentSize;
                    size.Width = 0;
                    return size;
                }

                if (_isBeingEdited &&
                    Window != null &&
                    Window.FieldEditor(false, this) is NSTextView fieldEditor &&
                    fieldEditor.TextContainer is NSTextContainer textContainer &&
                    textContainer.LayoutManager is NSLayoutManager layoutManager)
                {
                    var newWidth = layoutManager.GetUsedRectForTextContainer(textContainer).Width;
                    var newSize = base.IntrinsicContentSize;
                    newSize.Width = newWidth;

                    _lastSize = newSize;
                    return newSize;
                }
                else
                    return _lastSize ?? minSize();
            }
        }

        CGSize SizeForProgrammaticText(string text)
        {
            var font = Font ?? NSFont.SystemFontOfSize(NSFont.SystemFontSize, NSFontWeight.Regular);
            var attributes = new NSStringAttributes { Font = font };
            var stringWidth = new NSAttributedString(text, attributes).Size.Width;

            var size = base.IntrinsicContentSize;
            size.Width = stringWidth;
            return size;
        }

        public override void DidBeginEditing(NSNotification notification)
        {
            base.DidBeginEditing(notification);
            _isBeingEdited = true;
        }

        public override void DidEndEditing(NSNotification notification)
        {
            base.DidEndEditing(notification);
            _isBeingEdited = false;
        }

        public override void DidChange(NSNotification notification)
        {
            base.DidChange(notification);
            InvalidateIntrinsicContentSize();
        }
    }
}
