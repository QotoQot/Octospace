using System;
using System.Collections.Generic;
using AppKit;
using MvvmCross.Platforms.Mac.Presenters;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.Presenters;
using MvvmCross.ViewModels;

namespace Mac.MvvmCross.Presenters
{
    public interface IMvxAltMacViewPresenter : IMvxMacViewPresenter
    {
        public NSWindow MainWindow { get; }
        bool ShowPreviouslyOpenedWindow<T>() where T : MvxViewModel;
    }
}
