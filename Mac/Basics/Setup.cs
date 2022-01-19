using System;
using AppKit;
using Core;
using Core.Logging;
using Core.Model.Services.Documents.Markdown;
using Core.Model.State;
using Mac.MvvmCross.Bindings;
using Mac.MvvmCross.Presenters;
using Mac.Platform.Services.Documents.Markdown;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Diagnostics;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.IoC;
using MvvmCross.Platforms.Mac.Core;
using MvvmCross.Platforms.Mac.Presenters;
using MvvmCross.ViewModels;

namespace Mac
{
    public class Setup : MvxMacSetup<App>
    {
        protected override IMvxMacViewPresenter CreateViewPresenter()
        {
            Guard.IsNotNull(ApplicationDelegate, nameof(ApplicationDelegate));
            return new MvxAltMacViewPresenter(ApplicationDelegate);
        }

        protected override void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeLastChance(iocProvider);

            var state = Mvx.IoCProvider.Resolve<IState>();
            Mvx.IoCProvider.RegisterSingleton<IMarkdownService>(new MacMarkdownService(state));
            Mvx.IoCProvider.RegisterSingleton<IMvxMacViewDirector>(new MvxMacViewDirector());
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterPropertyInfoBindingFactory(
                typeof(NSComboBoxStringValueTargetBinding),
                typeof(NSComboBox),
                NSComboBoxStringValueTargetBinding.PropertyName);

            registry.RegisterPropertyInfoBindingFactory(
                typeof(NSPopUpButtonIndexOfSelectedItemTargetBinding),
                typeof(NSPopUpButton),
                NSPopUpButtonIndexOfSelectedItemTargetBinding.PropertyName);
        }

        protected override ILoggerProvider? CreateLogProvider() => new MvxLoggerProvider();

        protected override ILoggerFactory? CreateLogFactory() => new MvxLoggerFactory();
    }
}
