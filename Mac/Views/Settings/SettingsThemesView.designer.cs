// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mac.Views.Settings
{
	[Register ("SettingsThemesView")]
	partial class SettingsThemesView
	{
		[Outlet]
		AppKit.NSTextField ColorModeLabel { get; set; }

		[Outlet]
		AppKit.NSPopUpButton ColorModePopup { get; set; }

		[Outlet]
		AppKit.NSButton ContentThemesFollowAppThemesBtn { get; set; }

		[Outlet]
		AppKit.NSTabView Tabs { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Tabs != null) {
				Tabs.Dispose ();
				Tabs = null;
			}

			if (ContentThemesFollowAppThemesBtn != null) {
				ContentThemesFollowAppThemesBtn.Dispose ();
				ContentThemesFollowAppThemesBtn = null;
			}

			if (ColorModeLabel != null) {
				ColorModeLabel.Dispose ();
				ColorModeLabel = null;
			}

			if (ColorModePopup != null) {
				ColorModePopup.Dispose ();
				ColorModePopup = null;
			}
		}
	}
}
