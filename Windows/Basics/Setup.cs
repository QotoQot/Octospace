using Core.Logging;
using Microsoft.Extensions.Logging;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Presenters;
using MvvmCross.Platforms.Uap.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win.MvvmCross.Presenters;

namespace Win
{
#nullable enable
    public class Setup : MvxWindowsSetup<Core.App>
    {
        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame) => new MvxAltWindowsViewPresenter(rootFrame);

        protected override void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeLastChance(iocProvider);
            Mvx.IoCProvider.RegisterSingleton<IMvxWindowsViewDirector>(new MvxWindowsViewDirector());
        }

        protected override ILoggerProvider? CreateLogProvider() => new MvxLoggerProvider();

        protected override ILoggerFactory? CreateLogFactory() => new MvxLoggerFactory();
    }
#nullable restore
}
