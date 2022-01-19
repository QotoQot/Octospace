using System;
using System.Drawing;
using AppKit;
using Core.Logging;
using Core.Model.State;
using Foundation;
using MvvmCross;

namespace Mac.Views.Content.Documents.Pages
{
    [Register("PageTableClipView")]
    public class PageTableClipView : NSClipView
    {
        TextSettings _textSettings = null!;

        [Export("initWithCoder:")]
        public PageTableClipView(NSCoder coder) : base(coder) => Initialize();
        public PageTableClipView(IntPtr handle) : base(handle) => Initialize();
        public PageTableClipView(RectangleF frame) : base(frame) => Initialize();

        void Initialize() => _textSettings = Mvx.IoCProvider.Resolve<IState>().Text;

        public override NSEdgeInsets ContentInsets
        {
            get => base.ContentInsets;
            set
            {
                var maximumTableWidth = (int)_textSettings.PageLineWidth + TextBlockTableCellView.LeftPadding;

                if (DocumentView != null)
                {
                    var padding = (int)((Bounds.Width - maximumTableWidth) * 0.5f);
                    if (padding < 0)
                        padding = 0;

                    base.ContentInsets = new NSEdgeInsets(value.Top, padding, value.Bottom, padding);
                }
                else
                    base.ContentInsets = value;
            }
        }

        public void SetTextSettings(TextSettings textSettings) => _textSettings = textSettings;
    }
}
