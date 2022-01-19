using Core.Model.Content.Documents;
using Core.Model.State;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;

namespace Core.ViewModels.Content.Documents
{
    // FROM NOTE
    // TODO: when clicking on an unknown document name, get type before forming a request
    // probably a request that checks if the document exists and accessible in the first place
    // and only then asks for navigation

    public abstract class BlockViewModel<TBlock> : MvxViewModel<TBlock>, IBlockViewModel
        where TBlock : IBlock
    {
        protected BlockViewModel(IState state, IMvxMessenger messenger)
        {
            State = state;
            Messenger = messenger;
        }

        public IState State { get; }

        public IMvxMessenger Messenger { get; }

        public Type ParentDocumentType { get; set; } = null!;

        bool _isSelected;
        public bool IsSelected { get => _isSelected; set => SetProperty(ref _isSelected, value); }
    }
}
