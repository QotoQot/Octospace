using AppKit;
using Core.Resources.Localization;
using Core.ViewModels.Settings;
using Foundation;
using Mac.Platform.Views;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mac.Views.Settings
{
    public class SettingsToolbar : ToolbarDelegate
    {
        readonly Dictionary<string, string> _titles = new();

        public SettingsToolbar(NSWindow window, ReadOnlyCollection<SettingsSectionViewModel> sections) : base(window)
        {
            window.TitlebarAppearsTransparent = false;
            window.TitleVisibility = NSWindowTitleVisibility.Visible;

            if (IsModernStyle)
                window.ToolbarStyle = NSWindowToolbarStyle.Preference;

            var toolbar = window.Toolbar;
            Guard.IsNotNull(toolbar, nameof(toolbar));

            toolbar.Delegate = this;
            toolbar.AllowsUserCustomization = false;
            toolbar.DisplayMode = NSToolbarDisplayMode.IconAndLabel;
            toolbar.SizeMode = NSToolbarSizeMode.Default;
            toolbar.ShowsBaselineSeparator = true;

            int index = 0;
            toolbar.InsertItem(NSToolbar.NSToolbarFlexibleSpaceItemIdentifier, index++);

            foreach (var item in sections)
            {
                var identifier = item.GetType().Name;
                _titles[identifier] = item.Title;
                toolbar.InsertItem(identifier, index++);
            }

            toolbar.InsertItem(NSToolbar.NSToolbarFlexibleSpaceItemIdentifier, index++);
        }

        public void SelectItem(string identifier)
        {
            if (_windowRef.TryGetTarget(out NSWindow window) && window.Toolbar is NSToolbar toolbar)
                toolbar.SelectedItemIdentifier = identifier;
        }

        protected override NSToolbarItem CreateCustomItem(string identifier)
        {
            return new NSToolbarItem(identifier)
            {
                Label = _titles[identifier],
                Image = IconForItem(identifier),
            };
        }

        NSImage IconForItem(string identifier)
        {
            if (identifier == nameof(SettingsGeneralViewModel))
                return NSImage.ImageNamed("NSAdvanced");
            else if (identifier == nameof(SettingsTextViewModel))
                return NSImage.ImageNamed("NSAdvanced");
            else if (identifier == nameof(SettingsThemesViewModel))
                return NSImage.ImageNamed("NSAdvanced");

            return NSImage.ImageNamed("NSAdvanced");
        }


        #region INSToolbarDelegate

        [Export("toolbarSelectableItemIdentifiers:")]
        public string[] SelectableItemIdentifiers(NSToolbar toolbar)
        {
            return toolbar.Items
                .Where(item => item.Identifier != NSToolbar.NSToolbarFlexibleSpaceItemIdentifier)
                .Select(item => item.Identifier)
                .ToArray();
        }
        #endregion
    }
}
