using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels.MainWindow
{
    public class PaneViewModel : MvxViewModel
    {
        readonly IMvxViewModelLoader _viewModelLoader;

        public PaneViewModel(IMvxViewModelLoader viewModelLoader)
        {
            _viewModelLoader = viewModelLoader;
        }

        public MvxObservableCollection<SingleTabViewModel> Tabs { get; } = new();

        public bool IsFocused => Tabs.Any(item => item.IsSelected && item.IsFocused);

        public override async Task Initialize()
        {
            await base.Initialize();
            // TODO: restore opened tabs
        }

        public bool FocusInPreviouslyOpenedTab(TabContentRequest request)
        {
            //foreach (var item in Tabs)
            //{
            //    // TODO: compare content types?
            //    // TODO: changed pane/tab focus should happen via the bindings
            //}

            return false;
        }

        public void ReplaceFocusedTabContent(TabContentRequest request)
        {
            var tab = Tabs.FirstOrDefault(item => item.IsSelected);
            if (tab == null)
            {
                OpenNewTab(request);
                return;
            }

            tab.RequestContent(request);
        }

        public void OpenNewTab(TabContentRequest request)
        {
            SingleTabViewModel tab;

            if (request.DisplayMode == TabDisplayMode.CreateNewInBackground)
                tab = CreateTab(false, false);
            else
                tab = CreateTab(true, true);

            tab.RequestContent(request);
        }

        SingleTabViewModel CreateTab(bool selected, bool focused)
        {
            var tab = _viewModelLoader.LoadViewModel<SingleTabViewModel>();
            tab.IsSelected = selected;
            tab.IsFocused = focused;
            Tabs.Add(tab);
            return tab;
        }
    }
}
