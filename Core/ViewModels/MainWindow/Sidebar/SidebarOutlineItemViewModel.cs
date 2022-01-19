using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Resources.Localization;
using Core.ViewModels.Content;
using MvvmCross.ViewModels;

namespace Core.ViewModels.MainWindow
{
    public class SidebarOutlineItemViewModel : MvxViewModel
    {
        public static SidebarOutlineItemViewModel CreateHeader(string title) => new(SidebarOutlineItemType.Header, title);

        public static SidebarOutlineItemViewModel CreateTag(string title, bool isExpanded) => new(SidebarOutlineItemType.Tag, title, isExpanded);

        public static SidebarOutlineItemViewModel CreateButton(SidebarOutlineItemType type)
        {
            if (type == SidebarOutlineItemType.Error || type == SidebarOutlineItemType.Header || type == SidebarOutlineItemType.Tag)
                throw new ArgumentException($"Wrong sidebar outline button type: {type}");

            return new SidebarOutlineItemViewModel(type, LabelForType(type));
        }
        
        readonly List<SidebarOutlineItemViewModel> _children = new();

        // TEST: tag name should have no spaces
        SidebarOutlineItemViewModel(SidebarOutlineItemType type, string title, bool isExpanded = false)
        {
            Type = type;
            Title = title;
            IsExpanded = isExpanded;
        }

        public SidebarOutlineItemType Type { get; }
        public string Title { get; }
        public bool IsExpanded { get; }
        public bool HasChildren => _children.Count > 0;

        public ReadOnlyCollection<SidebarOutlineItemViewModel> Children => _children.AsReadOnly();

        public void SetupChildren(List<SidebarOutlineItemViewModel> children)
        {
            _children.Clear();
            _children.AddRange(children);
        }

        public static string LabelForType(SidebarOutlineItemType type)
        {
            return type switch
            {
                SidebarOutlineItemType.Tag => TextProvider.Get(ResX.MainWindow, "Sidebar.Buttons.Tag"),
                SidebarOutlineItemType.AllDocuments => TextProvider.Get(ResX.MainWindow, "Sidebar.Buttons.AllDocuments"),
                SidebarOutlineItemType.Favorites => TextProvider.Get(ResX.MainWindow, "Sidebar.Buttons.Favorites"),
                SidebarOutlineItemType.Trash => TextProvider.Get(ResX.MainWindow, "Sidebar.Buttons.Trash"),
                SidebarOutlineItemType.Random => TextProvider.Get(ResX.MainWindow, "Sidebar.Buttons.Random"),
                SidebarOutlineItemType.Daily => TextProvider.Get(ResX.MainWindow, "Sidebar.Buttons.Daily"),
                SidebarOutlineItemType.NewDocument => TextProvider.Get(ResX.MainWindow, "Sidebar.Buttons.NewDocument"),
                _ => throw new InvalidOperationException($"No label for sidebar outline item type: {type}")
            };
        }
    }
}
