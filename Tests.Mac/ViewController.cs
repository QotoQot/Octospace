using System;

using AppKit;
using Foundation;

using NUnit.Common;
using NUnitLite;
using System.Reflection;

namespace Tests.Mac
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.

            //var writer = new ExtendedTextWrapper(Console.Out);
            //var assembly = Assembly.GetExecutingAssembly();
            //new AutoRun(assembly).Execute(new string[] { }, writer, Console.In);
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
