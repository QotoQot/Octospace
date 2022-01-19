using AppKit;
using Core.Logging;
using Foundation;
using Microsoft.Toolkit.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace Mac.Platform.Views
{
    // https://mackuba.eu/notes/wwdc20/adopt-new-look-of-macos/

    public abstract class ToolbarDelegate : NSObject, INSToolbarDelegate
    {
        protected readonly WeakReference<NSWindow> _windowRef;

        public ToolbarDelegate(NSWindow window)
        {
            _windowRef = new WeakReference<NSWindow>(window);
        }

        protected bool IsModernStyle => DeviceInfo.Version.Major >= 11;

        public event EventHandler? ItemActivated;

        protected virtual NSToolbarItem CreateCustomItem(string identifier) => throw new NotImplementedException();
        
        void ToolbarItem_Activated(object sender, EventArgs e) => ItemActivated?.Invoke(sender, e);

        [Export("toolbar:itemForItemIdentifier:willBeInsertedIntoToolbar:")]
        public NSToolbarItem WillInsertItem(NSToolbar toolbar, string itemIdentifier, bool willBeInserted)
        {
            var item = CreateCustomItem(itemIdentifier);
            item.Activated += ToolbarItem_Activated;
            return item;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_windowRef.TryGetTarget(out NSWindow window) && window.Toolbar is NSToolbar toolbar)
                {
                    foreach (var item in toolbar.Items)
                        item.Activated -= ToolbarItem_Activated;
                }
            }

            base.Dispose(disposing);
        }
    }
}
