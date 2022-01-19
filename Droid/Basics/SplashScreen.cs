using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Core;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;
using Xamarin.Essentials;

namespace Droid
{
    [Activity(Label = "Octospace",
        Theme = "@style/AppTheme",
        MainLauncher = true,
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)] // future: any orientation
    public class SplashScreen : MvxSplashScreenActivity<MvxAndroidSetup<App>, App>
    {
        public SplashScreen() : base(Resource.Layout.Basics_SplashScreen) { }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //AppCenter.Start("99c3b1b8-33d1-40bd-835d-c8891d9c456d", typeof(Analytics), typeof(Crashes));
            Platform.Init(this, bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
