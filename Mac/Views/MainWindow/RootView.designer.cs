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
	[Register ("RootView")]
	partial class RootView
	{
		[Outlet]
		AppKit.NSView DetailContainer { get; set; }

		[Outlet]
		AppKit.NSView SidebarContainer { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SidebarContainer != null) {
				SidebarContainer.Dispose ();
				SidebarContainer = null;
			}

			if (DetailContainer != null) {
				DetailContainer.Dispose ();
				DetailContainer = null;
			}
		}
	}
}
