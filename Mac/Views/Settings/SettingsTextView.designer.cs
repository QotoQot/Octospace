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
	[Register ("SettingsTextView")]
	partial class SettingsTextView
	{
		[Outlet]
		AppKit.NSButton CheckOrthographyBtn { get; set; }

		[Outlet]
		AppKit.NSTextField EditingHeader { get; set; }

		[Outlet]
		AppKit.NSButton FetchLinkTitlesBtn { get; set; }

		[Outlet]
		AppKit.NSTextField FontHeader { get; set; }

		[Outlet]
		AppKit.NSPopUpButton FontNamePopup { get; set; }

		[Outlet]
		AppKit.NSButton InsertClosingBracketsBtn { get; set; }

		[Outlet]
		AppKit.NSComboBox PageFontSizeBox { get; set; }

		[Outlet]
		AppKit.NSTextField PageFontSizeLabel { get; set; }

		[Outlet]
		AppKit.NSComboBox PageLineSpacingBox { get; set; }

		[Outlet]
		AppKit.NSTextField PageLineSpacingLabel { get; set; }

		[Outlet]
		AppKit.NSTextField PageLineWidthLabel { get; set; }

		[Outlet]
		AppKit.NSPopUpButton PageLineWidthPopup { get; set; }

		[Outlet]
		AppKit.NSTextField PagesHeader { get; set; }

		[Outlet]
		AppKit.NSComboBox SpaceFontSizeBox { get; set; }

		[Outlet]
		AppKit.NSTextField SpaceFontSizeLabel { get; set; }

		[Outlet]
		AppKit.NSComboBox SpaceLineSpacingBox { get; set; }

		[Outlet]
		AppKit.NSTextField SpaceLineSpacingLabel { get; set; }

		[Outlet]
		AppKit.NSTextField SpacesHeader { get; set; }

		[Outlet]
		AppKit.NSButton UseSemiboldBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FontHeader != null) {
				FontHeader.Dispose ();
				FontHeader = null;
			}

			if (PagesHeader != null) {
				PagesHeader.Dispose ();
				PagesHeader = null;
			}

			if (SpacesHeader != null) {
				SpacesHeader.Dispose ();
				SpacesHeader = null;
			}

			if (EditingHeader != null) {
				EditingHeader.Dispose ();
				EditingHeader = null;
			}

			if (PageFontSizeLabel != null) {
				PageFontSizeLabel.Dispose ();
				PageFontSizeLabel = null;
			}

			if (PageLineSpacingLabel != null) {
				PageLineSpacingLabel.Dispose ();
				PageLineSpacingLabel = null;
			}

			if (PageLineWidthLabel != null) {
				PageLineWidthLabel.Dispose ();
				PageLineWidthLabel = null;
			}

			if (SpaceLineSpacingLabel != null) {
				SpaceLineSpacingLabel.Dispose ();
				SpaceLineSpacingLabel = null;
			}

			if (SpaceFontSizeLabel != null) {
				SpaceFontSizeLabel.Dispose ();
				SpaceFontSizeLabel = null;
			}

			if (CheckOrthographyBtn != null) {
				CheckOrthographyBtn.Dispose ();
				CheckOrthographyBtn = null;
			}

			if (FetchLinkTitlesBtn != null) {
				FetchLinkTitlesBtn.Dispose ();
				FetchLinkTitlesBtn = null;
			}

			if (FontNamePopup != null) {
				FontNamePopup.Dispose ();
				FontNamePopup = null;
			}

			if (InsertClosingBracketsBtn != null) {
				InsertClosingBracketsBtn.Dispose ();
				InsertClosingBracketsBtn = null;
			}

			if (PageFontSizeBox != null) {
				PageFontSizeBox.Dispose ();
				PageFontSizeBox = null;
			}

			if (PageLineSpacingBox != null) {
				PageLineSpacingBox.Dispose ();
				PageLineSpacingBox = null;
			}

			if (PageLineWidthPopup != null) {
				PageLineWidthPopup.Dispose ();
				PageLineWidthPopup = null;
			}

			if (SpaceFontSizeBox != null) {
				SpaceFontSizeBox.Dispose ();
				SpaceFontSizeBox = null;
			}

			if (SpaceLineSpacingBox != null) {
				SpaceLineSpacingBox.Dispose ();
				SpaceLineSpacingBox = null;
			}

			if (UseSemiboldBtn != null) {
				UseSemiboldBtn.Dispose ();
				UseSemiboldBtn = null;
			}
		}
	}
}
