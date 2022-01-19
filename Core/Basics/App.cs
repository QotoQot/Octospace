using Core.Logging;
using Core.ViewModels.MainWindow;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Microsoft.Extensions.Logging;
using Core.Resources.Themes.Content;
using Core.Resources.Themes.App;
using Core.MvvmCross;
using Core.Resources.Localization;
using Core.Model.State;
using MvvmCross.Binding;

#if DEBUG
using MvvmCross.Binding;
#endif

namespace Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            MvxLogger.LogLevel = LogLevel.Debug;
            MvxBindingLog.TraceBindingLevel = LogLevel.Warning;
#if DEBUG
            try
            {
                //Mvx.IoCProvider.RegisterSingleton<ICloudService>(new CloudService());
                //Mvx.IoCProvider.RegisterSingleton<IState>(new State(Mvx.IoCProvider.Resolve<IDatabaseService>(), CrossStoreReview.Current));
                Mvx.IoCProvider.RegisterSingleton(new ContentColors());
                Mvx.IoCProvider.RegisterSingleton(new AppTheme());

                Mvx.IoCProvider.RegisterSingleton<IState>(new State());
#endif
                // if no use history exist OR always show Home View on start
                //RegisterAppStart<HomeViewModel>();
                // else - navigate to the latest database and document
                RegisterAppStart<RootViewModel>();
#if DEBUG
            }
            catch (System.Exception ex)
            {
                Dlog.Info(ex);
            }
#endif
        }
    }
}

//namespace System.Runtime.CompilerServices
//{
//    public class IsExternalInit { }
//}