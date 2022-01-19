using Core.ViewModels.MainWindow;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win.MvvmCross.Presenters
{
    public interface IMvxWindowsViewDirector
    {
        void ShowView<T>() where T : MvxViewModel;
    }
}
