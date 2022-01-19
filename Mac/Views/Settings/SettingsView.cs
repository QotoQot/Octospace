using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using MvvmCross.Platforms.Mac.Views;
using Core.ViewModels.Settings;
using Mac.MvvmCross.Views;
using Mac.MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using Microsoft.Toolkit.Diagnostics;
using Core.Logging;
using CoreGraphics;

namespace Mac.Views.Settings
{
    [UniqueWindowPresentation("SettingsWindowCtrl")]
    public partial class SettingsView : MvxViewController<SettingsViewModel>, INSToolbarDelegate
    {
        readonly List<MvxViewController> _sections = new();
        SettingsToolbar? _toolbarDelegate;

        [Export("initWithCoder:")]
        public SettingsView(NSCoder coder) : base(coder) => Initialize();
        public SettingsView(IntPtr handle) : base(handle) => Initialize();
        public SettingsView() : base(nameof(SettingsView), NSBundle.MainBundle) => Initialize();
        void Initialize() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            foreach (var item in ViewModel.Sections)
            {
                var section = (MvxViewController)this.CreateViewControllerWithViewModel(item);
                _sections.Add(section);
            }
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();

            var window = View.Window;
            Guard.IsNotNull(window, nameof(window));

            if (_toolbarDelegate == null)
            {
                _toolbarDelegate = new(window, ViewModel.Sections);
                _toolbarDelegate.ItemActivated += ToolbarDelegate_ItemActivated;
            }

            var toolbar = window.Toolbar;
            Guard.IsNotNull(toolbar, nameof(toolbar));

            var firstItem = toolbar.Items.FirstOrDefault(item => item.Identifier == nameof(SettingsTextViewModel));
            //var firstItem = toolbar.Items.FirstOrDefault(item => item.Identifier == nameof(SettingsGeneralViewModel));
            Guard.IsNotNull(firstItem, nameof(firstItem));
            ShowSection(firstItem);
        }

        void ShowSection(NSToolbarItem toolbarItem)
        {
            View.Window.Title = toolbarItem.Label;
            _toolbarDelegate?.SelectItem(toolbarItem.Identifier);

            var childrenCount = View.Subviews.Length;
            for (int i = childrenCount - 1; i >= 0; i--)
                View.Subviews[i].RemoveFromSuperview();

            var section = _sections.FirstOrDefault(item => item.ViewModel.GetType().Name == toolbarItem.Identifier);
            Guard.IsNotNull(section, nameof(section));

            section.View.Frame = new CGRect(CGPoint.Empty, section.View.Bounds.Size);
            View.Window.SetContentSize(section.View.Bounds.Size);
            View.AddSubview(section.View);
        }

        void ToolbarDelegate_ItemActivated(object sender, EventArgs e)
        {
            if (sender is NSToolbarItem toolbarItem)
                ShowSection(toolbarItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_toolbarDelegate != null)
                    _toolbarDelegate.ItemActivated -= ToolbarDelegate_ItemActivated;

                _sections.Clear();
            }

            base.Dispose(disposing);
        }
    }
}
