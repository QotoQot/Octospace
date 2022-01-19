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
	[Register ("TextBlockTableCellView")]
	partial class TextBlockTableCellView
	{
		[Outlet]
		TextBlockTextView TextView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TextView != null) {
				TextView.Dispose ();
				TextView = null;
			}
		}
	}
}
