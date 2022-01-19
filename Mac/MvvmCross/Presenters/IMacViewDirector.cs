using Core.ViewModels.Content;
using Core.ViewModels.MainWindow;
using MvvmCross.ViewModels;
using System;
namespace Mac.MvvmCross.Presenters
{
    public interface IMvxMacViewDirector
    {
        void ShowView<T>() where T : MvxViewModel;
    }
}
