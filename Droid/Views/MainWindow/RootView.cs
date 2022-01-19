using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Core;
using Core.ViewModels.MainWindow;
using Droid.MvvmCross.Views;
using MvvmCross;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views;
using System;



namespace Droid.Views.MainWindow
{
    [Activity(Label = "Octospace",
        Theme = "@style/AppTheme",
        ScreenOrientation = ScreenOrientation.Portrait)] // future: any orientation
    public class RootView : MvxActivity<RootViewModel>
    {
        // https://medium.com/android-news/build-awesome-animations-with-7-lines-of-code-using-constraintlayout-854e8fd3ad93
        // https://proandroiddev.com/creating-awesome-animations-using-constraintlayout-and-constraintset-part-i-390cc72c5f75

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainWindow_RootView);

            // https://stackoverflow.com/questions/25430283/android-master-detail-flow-dual-pane-using-1-activity

            // TODO: no idea why it's not displayed
            var sidebar = (SidebarView)SupportFragmentManager.CreateFragmentWithViewModel(ViewModel.Sidebar);
            SupportFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.primary_container, sidebar)
                .DisallowAddToBackStack()
                .Commit();

            var detail = (DetailView)SupportFragmentManager.CreateFragmentWithViewModel(ViewModel.Detail);
            SupportFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.secondary_container, detail)
                .DisallowAddToBackStack()
                .Commit();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            //InAppBillingImplementation.HandleActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            //Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

        }
    }
}
