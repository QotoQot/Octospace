using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Win.Basics
{
    public class ThemeDelegate
    {
        Window _window;

        public ThemeDelegate(Window window)
        {

        }

        // TODO: Core theme enum, not Windows-based one
        // copy the long answer from here: https://stackoverflow.com/a/56918252/1014048
        void ApplyTheme(ElementTheme theme)
        {
            // Set theme for window root.
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                frameworkElement.RequestedTheme = theme;
            }
        }
    }
}
