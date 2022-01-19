// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mac.Views.Content.Documents.Pages
{
	[Register ("PageView")]
	partial class PageView
	{
		[Outlet]
		Mac.Views.Content.Documents.Pages.PageTableClipView TableClipView { get; set; }

		[Outlet]
		AppKit.NSTableView TableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}

			if (TableClipView != null) {
				TableClipView.Dispose ();
				TableClipView = null;
			}
		}
	}
}
