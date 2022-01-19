using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CoreGraphics;

namespace Mac.Platform.Views
{
    public partial class TransparentScroller : NSScroller
    {
        [Export("initWithCoder:")]
        public TransparentScroller(NSCoder coder) : base(coder) => Initialize();
        public TransparentScroller(IntPtr handle) : base(handle) => Initialize();
        void Initialize() { }

        public override void DrawRect(CGRect dirtyRect) => DrawKnob();
    }
}
