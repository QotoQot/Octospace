using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Logging;
using Core.ViewModels.Content;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels.MainWindow
{
    public class SidebarViewModel : MvxViewModel
    {
        public SidebarViewModel()
        {
            Dlog.Info("SIDEBAR VM constructor");
            ShowSelectedItemCommand = new MvxAsyncCommand<SidebarOutlineItemViewModel>(ShowSelectedItem);
            _items = new List<SidebarOutlineItemViewModel>();
        }

        public IMvxAsyncCommand<SidebarOutlineItemViewModel> ShowSelectedItemCommand { get; }

        // TODO: rework into MvxObservableCollection's with ReplaceWith()
        // what to do with AllItems though? copypaste probably on any change
        List<SidebarOutlineItemViewModel> _items;
        public List<SidebarOutlineItemViewModel> Items { get => _items; private set => SetProperty(ref _items, value); }

        public override void Prepare()
        {
            base.Prepare();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            Dlog.Info("SIDEBAR VM init");

            await UpdateItems();
        }

        async Task UpdateItems()
        {
            var items = new List<SidebarOutlineItemViewModel>
            {
                SidebarOutlineItemViewModel.CreateButton(SidebarOutlineItemType.AllDocuments),
                SidebarOutlineItemViewModel.CreateButton(SidebarOutlineItemType.Favorites),
                SidebarOutlineItemViewModel.CreateButton(SidebarOutlineItemType.Trash),
                SidebarOutlineItemViewModel.CreateButton(SidebarOutlineItemType.Random)
            };
            
            var tagHeader = SidebarOutlineItemViewModel.CreateHeader("Tags");
            items.Add(tagHeader);

            // TODO: real ones
            // TODO: sort a-z
            // TODO: restore collapsed tags
            var tempIsExpanded = true;
            items.Add(SidebarOutlineItemViewModel.CreateTag("blog", tempIsExpanded));
            items.Add(SidebarOutlineItemViewModel.CreateTag("house", tempIsExpanded));
            items.Add(SidebarOutlineItemViewModel.CreateTag("misc", tempIsExpanded));
            items.Add(SidebarOutlineItemViewModel.CreateTag("tech", tempIsExpanded));

            var secondLevelChildren = new List<SidebarOutlineItemViewModel>()
            {
                SidebarOutlineItemViewModel.CreateTag("test_tag", tempIsExpanded),
                SidebarOutlineItemViewModel.CreateTag("another_test_tag", tempIsExpanded),
            };

            items[items.Count - 2].SetupChildren(secondLevelChildren);

            var thirdLevelChildren = new List<SidebarOutlineItemViewModel>() { SidebarOutlineItemViewModel.CreateTag("third-level", tempIsExpanded) };
            secondLevelChildren[0].SetupChildren(thirdLevelChildren);

            var fourthLevelChildren = new List<SidebarOutlineItemViewModel>() { SidebarOutlineItemViewModel.CreateTag("fourth_level", tempIsExpanded) };
            thirdLevelChildren[0].SetupChildren(fourthLevelChildren);

            var fifthLevelChildren = new List<SidebarOutlineItemViewModel>() { SidebarOutlineItemViewModel.CreateTag("fifth_level", tempIsExpanded) };
            fourthLevelChildren[0].SetupChildren(fifthLevelChildren);

            var sixthLevelChildren = new List<SidebarOutlineItemViewModel>() { SidebarOutlineItemViewModel.CreateTag("fifth_level", tempIsExpanded) };
            fifthLevelChildren[0].SetupChildren(sixthLevelChildren);

            Items = items;
        }

        // TODO: readonly lists?
        async Task ShowSelectedItem(SidebarOutlineItemViewModel itemViewModel)
        {
            // TODO: other modes depending on hotkeys, settings, etc.
            // TODO: tags
            //var tabContentRequest = new TabContentRequest(TabDisplayMode.NewOrOpened, itemViewModel.ContentType, itemViewModel.Name);

            
        }
    }
}
