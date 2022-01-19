using Core.ViewModels.MainWindow;
using Foundation;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.ViewModels;
using System;

namespace Mac.View.Content
{
    public class TabContentView<TViewModel> : MvxViewController
        where TViewModel : class, IMvxViewModel, ITabContentViewModel
    {
        public TabContentView(IntPtr handle) : base(handle) { }
        public TabContentView(NSCoder coder) : base(coder) { }
        public TabContentView(string viewName, NSBundle bundle) : base(viewName, bundle) { }
        public TabContentView(string viewName) : base(viewName, NSBundle.MainBundle) { }
        public TabContentView() : base() { }

        public new TViewModel? ViewModel
        {
            get => (TViewModel?)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
