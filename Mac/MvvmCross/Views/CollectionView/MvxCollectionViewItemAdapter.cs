using System;
using MvvmCross.Platforms.Mac.Views;
using MvvmCross.Platforms.Mac.Views.Base;
using MvvmCross.Views;

namespace Mac.MvvmCross.Views
{
    public class MvxCollectionViewItemAdapter : MvxBaseViewControllerAdapter
    {
        protected IMvxMacView? MacView
        {
            get { return ViewController as IMvxMacView; }
        }

        public MvxCollectionViewItemAdapter(IMvxEventSourceViewController eventSource) : base(eventSource)
        {
            if (!(eventSource is IMvxMacView))
                throw new ArgumentException("eventSource", "eventSource should be a IMvxMacView");
        }

        public override void HandleViewDidLoadCalled(object sender, EventArgs e)
        {
            // removed MacView.OnViewCreate(); call because view models are set manually
            base.HandleViewDidLoadCalled(sender, e);
        }

        public override void HandleDisposeCalled(object sender, EventArgs e)
        {
            MacView?.OnViewDestroy();
            base.HandleDisposeCalled(sender, e);
        }
    }
}
