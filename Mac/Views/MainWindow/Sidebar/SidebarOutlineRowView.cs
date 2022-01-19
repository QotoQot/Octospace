using AppKit;
using CoreGraphics;
using Foundation;
using System;
using System.Drawing;

namespace Mac.Views.MainWindow
{
    [Register("SidebarOutlineRowView")]
    public class SidebarOutlineRowView : NSTableRowView
    {
        [Export("initWithCoder:")]
        public SidebarOutlineRowView(NSCoder coder) : base(coder) => Initialize();
        public SidebarOutlineRowView(IntPtr handle) : base(handle) => Initialize();
        public SidebarOutlineRowView(RectangleF frame) : base(frame) => Initialize();
        public SidebarOutlineRowView() : base() => Initialize();

        void Initialize()
        {
            
		}

        // does not work for source lists

        //public override void DrawSelection(CGRect dirtyRect)
        //{
        //    base.DrawSelection(dirtyRect);

        //    //var rect = new CGRect();
        //    var path = NSBezierPath.FromRect(dirtyRect);

        //    // TODO: theme color
        //    NSColor.SystemPurpleColor.Set();
        //    path.Fill();
        //    path.Stroke();
        //}
    }
}
