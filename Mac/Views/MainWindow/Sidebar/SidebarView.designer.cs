// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mac.Views.MainWindow
{
	[Register ("SidebarView")]
	partial class SidebarView
	{
		[Outlet]
		AppKit.NSOutlineView OutlineView { get; set; }

		[Outlet]
		AppKit.NSView TransparentOverlay { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TransparentOverlay != null) {
				TransparentOverlay.Dispose ();
				TransparentOverlay = null;
			}

			if (OutlineView != null) {
				OutlineView.Dispose ();
				OutlineView = null;
			}
		}
	}
}
