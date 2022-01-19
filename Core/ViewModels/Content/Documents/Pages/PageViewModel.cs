using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Core.Basics.Utils;
using Core.Logging;
using Core.Model.Content.Documents;
using Core.Model.Services.Documents;
using Core.MvvmCross;
using Core.ViewModels.Content.Documents;
using Core.ViewModels.MainWindow;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels.Content.Documents
{
    public class PageViewModel : MvxViewModel<DocumentName>, ITabContentViewModel
    {
        readonly IMvxViewModelLoader _viewModelLoader;
        readonly IDocumentService _documentService;

        // TODO: clear on delete, deselect all
        //readonly List<int> _selectedIndexes = new();

        Page? _page;

        public PageViewModel(IMvxViewModelLoader viewModelLoader, IDocumentService documentService)
        {
            _viewModelLoader = viewModelLoader;
            _documentService = documentService;
        }

        public DocumentName? DocumentName { get; private set; }

        public string? Title => DocumentName?.Value;

        public TabContentRequest? TabContentRequest { get; private set; }

        public MvxObservableCollection<IBlockViewModel> Blocks { get; } = new();

        public override void Prepare(DocumentName documentName) => DocumentName = documentName;

        public async override Task Initialize()
        {
            await base.Initialize();
            Guard.IsNotNull(DocumentName, nameof(DocumentName));

            try { _page = (Page)await _documentService.LoadDocument(DocumentName); }
            catch (Exception ex) { Dlog.Info(ex); }

            SetupViewModel();

            Blocks.CollectionChanged += Blocks_CollectionChanged;
        }

        private void Blocks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TODO: unclear why new items are null
        }

        public void UpdateSelection(HashSet<int> selectedIndexes)
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                var item = Blocks[i];
                item.IsSelected = selectedIndexes.Contains(i);
            }
        }

        public void RearrangeBlocks(HashSet<int> selectedIndexes, int targetIndex)
        {
            var rearrangedBlocks = new List<IBlockViewModel>(Blocks);

            var test = rearrangedBlocks.MovedItems(selectedIndexes, targetIndex);

            if (rearrangedBlocks.Count != Blocks.Count)
                throw new InvalidOperationException("Rearranged blocks do not have the same count");

            Blocks.ReplaceWith(rearrangedBlocks);
        }

        void SetupViewModel()
        {
            Guard.IsNotNull(_page, nameof(_page));

            var blocks = new List<IBlockViewModel>();

            foreach (var item in _page.Blocks)
            {
                if (item is TextBlock textBlock)
                {
                    var blockViewModel = _viewModelLoader.LoadViewModel<TextBlockViewModel>();
                    blockViewModel.ParentDocumentType = typeof(Page);
                    blockViewModel.Prepare(textBlock);
                    blocks.Add(blockViewModel);
                }
            }

            Blocks.ReplaceWith(blocks);
        }
    }
}
