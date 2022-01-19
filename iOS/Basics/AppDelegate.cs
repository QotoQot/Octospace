using Core;
using Foundation;
using iOS.MvvmCross.Core;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Ios.Core;
using System;
using UIKit;

namespace iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            MvxIosSetupSingleton.EnsureSingletonAvailable(this, (UIWindow)null).EnsureInitialized();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            LifetimeChanged?.Invoke(this, new MvxLifetimeEventArgs(MvxLifetimeEvent.Launching));

            return true;
        }

        protected override void RunAppStart(object? hint = null) { }

        new public event EventHandler<MvxLifetimeEventArgs>? LifetimeChanged;
    }
}
