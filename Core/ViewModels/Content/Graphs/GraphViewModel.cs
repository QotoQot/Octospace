using System;
using Core.ViewModels.MainWindow;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels.Content
{
    public class GraphViewModel : MvxViewModel, ITabContentViewModel
    {
        public GraphViewModel()
        {
        }

        public TabContentRequest? TabContentRequest { get; private set; }

        public string? Title => throw new NotImplementedException();
    }
}
