using System;
using Core.ViewModels.MainWindow;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels.Content.Documents
{
    public class SpaceViewModel : MvxViewModel, ITabContentViewModel
    {
        // TODO: async loading
        // skeleton comes first, then async request for data

        public SpaceViewModel()
        {

        }

        public TabContentRequest? TabContentRequest { get; private set; }

        public string? Title => throw new NotImplementedException();
    }
}
