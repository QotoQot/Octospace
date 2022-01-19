using NUnit.Common;
using NUnitLite;
using System;
using System.Reflection;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Controls;

namespace Tests.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var args = new string[] { };
            var writer = new ExtendedTextWrapper(Console.Out);
            var assembly = Assembly.GetExecutingAssembly();
            new AutoRun(assembly).Execute(args, writer, Console.In);
            CoreApplication.Exit();
        }
    }
}
