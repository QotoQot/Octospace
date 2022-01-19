using Core.Model.State;
using Core.Resources.Themes.App;
using Core.Resources.Themes.Content;
using System;
namespace Core.Resources.Themes
{
    public static class Themes
    {
        static Themes()
        {
            App = new();
            Content = new();
        }

        public static AppTheme App { get; }
        public static ContentColors Content { get; }
    }
}
