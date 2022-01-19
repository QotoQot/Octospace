using MvvmCross.ViewModels;
using System;

namespace Core.ViewModels.MainWindow
{
    public interface ITabContentViewModel : IMvxViewModel
    {
        string? Title { get; }
        TabContentRequest? TabContentRequest { get; }
    }
}
