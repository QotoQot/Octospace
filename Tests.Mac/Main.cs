using AppKit;
using NUnit.Common;
using NUnitLite;
using System;
using System.Reflection;

namespace Tests.Mac
{
    static class MainClass
    {
        //static void Main(string[] args)
        //{
        //    NSApplication.Init();
        //    NSApplication.Main(args);
        //}

        static int Main(string[] args)
        {
            args = new string[] { };
            var writer = new ExtendedTextWrapper(Console.Out);
            var assembly = Assembly.GetExecutingAssembly();
            return new AutoRun(assembly).Execute(args, writer, Console.In);
        }
    }
}
