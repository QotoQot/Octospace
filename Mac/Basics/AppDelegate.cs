using AppKit;
using Core.ViewModels.MainWindow;
using Foundation;
using Mac.MvvmCross.Presenters;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross;
using MvvmCross.Platforms.Mac.Core;

namespace Mac
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate<Setup, Core.App>
    {
        IMvxMacViewDirector _viewDirector = null!;
        IMvxMacViewDirector ViewDirector
        {
            get
            {
                Guard.IsNotNull(_viewDirector, nameof(_viewDirector));
                return _viewDirector;
            }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            MvxMacSetupSingleton.EnsureSingletonAvailable(this).EnsureInitialized();
            RunAppStart();

            _viewDirector = Mvx.IoCProvider.Resolve<IMvxMacViewDirector>();
        }

        public override bool ApplicationShouldHandleReopen(NSApplication sender, bool hasVisibleWindows)
        {
            if (hasVisibleWindows)
                return false;
            else
            {
                ViewDirector.ShowView<RootViewModel>();
                return true;
            }
        }
    }
}
