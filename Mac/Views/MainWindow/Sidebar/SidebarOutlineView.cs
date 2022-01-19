using AppKit;
using Core.Logging;
using CoreGraphics;
using Foundation;
using System;
using System.Drawing;

namespace Mac.Views.MainWindow
{
    [Register("SidebarOutlineView")]
    public class SidebarOutlineView : NSOutlineView
    {
        [Export("initWithCoder:")]
        public SidebarOutlineView(NSCoder coder) : base(coder) => Initialize();
        public SidebarOutlineView(IntPtr handle) : base(handle) => Initialize();
        public SidebarOutlineView(RectangleF frame) : base(frame) => Initialize();

        void Initialize()
        {
            
        }

        // doesn't work for some reason

        //public override CGRect GetCellFrame(nint column, nint row)
        //{
        //    var regularFrame = base.GetCellFrame(column, row);
        //    if (row == 0)
        //        return new CGRect(0, regularFrame.Y, Bounds.Width, regularFrame.Height);
        //    else
        //        return regularFrame;
        //}
    }
}
