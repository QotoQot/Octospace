using System;
using System.Drawing;
using AppKit;
using Core.Logging;
using CoreGraphics;
using Foundation;
using Microsoft.Toolkit.Diagnostics;

namespace Mac.Views.Content.Documents.Pages
{
    [Register("PageBlockOverlay")]
    public class BlockOverlay : NSView
    {
        [Export("initWithCoder:")]
        public BlockOverlay(NSCoder coder) : base(coder) => Initialize();
        public BlockOverlay(IntPtr handle) : base(handle) => Initialize();
        public BlockOverlay(RectangleF frame) : base(frame) => Initialize();
        public BlockOverlay() : base() => Initialize();

        void Initialize()
        {
            WantsLayer = true;
            Guard.IsNotNull(Layer, nameof(Layer));

            // TODO: content theme colors
            Layer.BackgroundColor = CGColor.CreateSrgb(0.91f, 0.96f, 1, 0.75f);
            //Layer.BorderColor = CGColor.CreateSrgb(0, 0, 0, 1);
            //Layer.BorderWidth = 1;
            Layer.CornerRadius = 5;
            Layer.MasksToBounds = true;
        }
    }
}
