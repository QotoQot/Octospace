using Core;
using iOS.MvvmCross.Presenters;
using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Platforms.Ios.Presenters;
using Serilog;
using Serilog.Extensions.Logging;

namespace iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override IMvxIosViewPresenter CreateViewPresenter() => new MvxAltIosViewPresenter();

        protected override ILoggerProvider? CreateLogProvider() => new SerilogLoggerProvider();

        protected override ILoggerFactory? CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
    }
}
