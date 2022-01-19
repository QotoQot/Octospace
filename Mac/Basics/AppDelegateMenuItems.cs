using AppKit;
using Core.ViewModels.MainWindow;
using Core.ViewModels.Settings;
using Mac.Views.MainWindow;

namespace Mac
{
    public partial class AppDelegate
    {
        partial void MenuItem_Preferences(NSMenuItem sender)
        {
            // TODO: check for a window and do not navigate if opened
            ViewDirector.ShowView<SettingsViewModel>();
        }

        partial void MenuItem_NewDocument(NSMenuItem sender)
        {
            
        }

        partial void MenuItem_NewDatabase(NSMenuItem sender)
        {
            
        }

        partial void MenuItem_CloseDocument(NSMenuItem sender)
        {
            var window = NSApplication.SharedApplication.MainWindow;

            if (window.ContentViewController is RootView)
            {
                // TODO: close a tab OR close the window too if it's the last tab
            }
            else
                window.Close();
        }

        partial void MenuItem_CloseDatabase(NSMenuItem sender)
        {
            
        }

        partial void MenuItem_NewTab(NSMenuItem sender)
        {
            
        }

        partial void MenuItem_NewWindow(NSMenuItem sender)
        {
            // TODO: position better
            //https://stackoverflow.com/questions/57546804/cascading-in-custom-nswindowcontroller
            //https://stackoverflow.com/questions/35827239/document-based-app-autosave-with-storyboards
            //https://stackoverflow.com/questions/55714660/how-position-and-cascade-new-document-windows-at-the-top-left-of-the-screen
            
            ViewDirector.ShowView<RootViewModel>();
        }
    }
}
