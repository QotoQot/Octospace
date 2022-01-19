using AppKit;
using Core.Logging;
using Core.Resources.Localization;
using Mac.Platform.Views;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Collections.Generic;

namespace Mac.Views.MainWindow
{
    // see here for popup menus, search, item centering, etc.
    // https://github.com/marioaguzman/toolbar

    public class RootToolbar : ToolbarDelegate
    {
        public const string NewDocumentId = "NewDocument";
        public const string ToggleSidebarId = "ToggleSidebar";
        public const string BackId = "Back";
        public const string ForwardId = "Forward";
        public const string FormattingPanelId = "FormattingPanel";
        public const string DocumentSettingsId = "DocumentSettings";

        public RootToolbar(NSWindow window) : base(window)
        {
            window.TitlebarAppearsTransparent = true;
            window.TitleVisibility = NSWindowTitleVisibility.Visible;

            // TODO: user settings
            if (IsModernStyle)
            {
                window.ToolbarStyle = NSWindowToolbarStyle.Unified;
            }

            var toolbar = window.Toolbar;
            Guard.IsNotNull(toolbar, nameof(toolbar));

            toolbar.Delegate = this;
            toolbar.AllowsUserCustomization = false;
            toolbar.DisplayMode = NSToolbarDisplayMode.Icon;
            toolbar.SizeMode = NSToolbarSizeMode.Small;
            toolbar.ShowsBaselineSeparator = false;

            int index = 0;
            toolbar.InsertItem(NSToolbar.NSToolbarFlexibleSpaceItemIdentifier, index++);
            toolbar.InsertItem(NewDocumentId, index++);
            //toolbar.InsertItem(ToggleSidebarId, index++);

            if (IsModernStyle)
                index++; // skip the separator

            toolbar.InsertItem(NSToolbar.NSToolbarToggleSidebarItemIdentifier, index++);

            toolbar.InsertItem(BackId, index++);
            toolbar.InsertItem(ForwardId, index++);
            toolbar.InsertItem(NSToolbar.NSToolbarFlexibleSpaceItemIdentifier, index++);
            toolbar.InsertItem(FormattingPanelId, index++);
            toolbar.InsertItem(DocumentSettingsId, index++);
        }

        protected override NSToolbarItem CreateCustomItem(string identifier)
        {
            return identifier switch
            {
                // TODO: accessibility to ResX
                // TODO: pre-11 images
                NewDocumentId => new NSToolbarItem(identifier)
                {
                    Bordered = true,
                    MenuFormRepresentation = new NSMenuItem("test"),
                    ToolTip = LabelForItem(identifier),
                    Image = IsModernStyle
                        ? NSImage.GetSystemSymbol("square.and.pencil", LabelForItem(identifier))
                        : NSImage.ImageNamed(""),
                },
                //ToggleSidebarId => new NSToolbarItem(identifier)
                //{
                //    Bordered = true,
                //    Navigational = true,
                //    Image = IsModernStyle
                //        ? NSImage.GetSystemSymbol("sidebar.leading", LabelForItem(identifier))
                //        : NSImage.ImageNamed(""),
                //},
                BackId => new NSToolbarItem(identifier)
                {
                    Bordered = true,
                    Navigational = true,
                    ToolTip = LabelForItem(identifier),
                    Image = IsModernStyle
                        ? NSImage.GetSystemSymbol("chevron.backward", LabelForItem(identifier))
                        : NSImage.ImageNamed(""),
                },
                ForwardId => new NSToolbarItem(identifier)
                {
                    Bordered = true,
                    Navigational = true,
                    ToolTip = LabelForItem(identifier),
                    Image = IsModernStyle
                        ? NSImage.GetSystemSymbol("chevron.forward", LabelForItem(identifier))
                        : NSImage.ImageNamed(""),
                },
                FormattingPanelId => new NSToolbarItem(identifier)
                {
                    Bordered = true,
                    ToolTip = LabelForItem(identifier),
                    Image = IsModernStyle
                        ? NSImage.GetSystemSymbol("textformat", LabelForItem(identifier))
                        : NSImage.ImageNamed(""),
                },
                DocumentSettingsId => new NSToolbarItem(identifier)
                {
                    Bordered = true,
                    ToolTip = LabelForItem(identifier),
                    Image = IsModernStyle
                        ? NSImage.GetSystemSymbol("ellipsis.circle", LabelForItem(identifier))
                        : NSImage.ImageNamed(""),
                },
                _ => throw new ArgumentException("Incorrect toolbar item identifier: " + identifier)
            };
        }

        string LabelForItem(string identifier) => TextProvider.Get(ResX.MainWindow, "Toolbar.Buttons." + identifier);
    }
}
