using System;
using System.Threading.Tasks;
using Core.Model.Content.Documents;
using Core.Model.Services;
using Core.MvvmCross;
using Core.ViewModels.Content;
using Core.ViewModels.Content.Documents;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels.MainWindow
{
    public class SingleTabViewModel : MvxViewModel
    {
        readonly IMvxViewModelLoader _viewModelLoader;

        public SingleTabViewModel(IMvxViewModelLoader viewModelLoader)
        {
            _viewModelLoader = viewModelLoader;
        }

        bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        bool _isFocused;
        public bool IsFocused
        {
            get => _isFocused;
            set => SetProperty(ref _isFocused, value);
        }

        ITabContentViewModel? _content;
        public ITabContentViewModel? Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public void RequestContent(TabContentRequest request)
        {
            Guard.IsNotNull(request.DocumentName, nameof(request.DocumentName));
            Content = request.ContentType switch
            {
                TabContentType.Page => _viewModelLoader.LoadViewModel<PageViewModel, DocumentName>(request.DocumentName),
                TabContentType.Space => _viewModelLoader.LoadViewModel<SpaceViewModel, DocumentName>(request.DocumentName),
                TabContentType.Graph => _viewModelLoader.LoadViewModel<GraphViewModel>(),
                //TabContentType.SearchResults => typeof(???ViewModel),
                _ => throw new ArgumentException("Tab can't load content for " + request.ContentType)
            };
        }
    }
}
