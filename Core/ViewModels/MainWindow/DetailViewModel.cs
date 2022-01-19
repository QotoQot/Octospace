using Core.Model.Content.Documents;
using Core.Model.Services;
using Core.MvvmCross;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ViewModels.MainWindow
{
    public class DetailViewModel : MvxViewModel
    {
        readonly IMvxViewModelLoader _viewModelLoader;

        public DetailViewModel(IMvxViewModelLoader viewModelLoader)
        {
            _viewModelLoader = viewModelLoader;
            
        }

        // is [MvxSetToNullAfterBinding] needed?
        public MvxObservableCollection<PaneViewModel> Panes { get; } = new();

        PaneViewModel FocusedPane => Panes.FirstOrDefault(item => item.IsFocused) ?? Panes.First();

        public async override Task Initialize()
        {
            await base.Initialize();

            // restore serialized pane layout
            // send it to ChangePresentation wrapped into a hint

            Panes.Add(_viewModelLoader.LoadViewModel<PaneViewModel>());
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            // TODO: restore opened panes

            var documentName = new DocumentName("TestPage");
            var contentRequest = new TabContentRequest(TabDisplayMode.TryToFocusInAlreadyOpened, TabContentType.Page, documentName);
            ShowTabContent(contentRequest);
        }

        public void ShowTabContent(TabContentRequest request)
        {
            if (request.DisplayMode == TabDisplayMode.TryToFocusInAlreadyOpened)
            {
                foreach (var item in Panes)
                {
                    if (item.FocusInPreviouslyOpenedTab(request))
                        return;
                }
            }

            switch (request.DisplayMode)
            {
                case TabDisplayMode.TryToFocusInAlreadyOpened:
                case TabDisplayMode.CreateNewInBackground:
                    FocusedPane.OpenNewTab(request);
                    break;
                case TabDisplayMode.ReplaceFocused:
                    FocusedPane.ReplaceFocusedTabContent(request);
                    break;
                case TabDisplayMode.SidePane: // TODO: new split view on the right
                    throw new NotImplementedException();
                case TabDisplayMode.Preview: // TODO: no idea for now
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("Trying to show tab content that has incorrect display mode: " + request.DisplayMode);
            }
        }
    }
}
