using Foundation;
using iOS.MvvmCross.Core;
using Microsoft.Toolkit.Diagnostics;
using System;
using UIKit;

namespace iOS
{
    public class SceneDelegate : MvxSceneDelegate
    {
        public override void WillConnect(UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions)
        {
            // Use this method to optionally configure and attach the UIWindow `window` to the provided UIWindowScene `scene`.
            // If using a storyboard, the `window` property will automatically be initialized and attached to the scene.
            // This delegate does not imply the connecting scene or session are new (see UIApplicationDelegate `GetConfiguration` instead).
        }
    }
}
