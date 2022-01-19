using Core.Logging;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.Generic;
using System.Text;

using Preferences = Xamarin.Essentials.Preferences;

namespace Core.Resources.Themes.App
{
    public interface IAppThemeDelegate
    {
        AppTheme.Mode SystemColorMode { get; }
        event Action<AppTheme.Mode> SystemColorModeChanged;
    }

    public partial class AppTheme
    {
        public enum Mode
        {
            Light,
            Dark
        }

        public enum ModeSetting
        {
            Auto = 0,
            Light = 1,
            Dark = 2
        }

        readonly IMvxMessenger _messenger;
        IAppThemeDelegate _delegate = null!;
        Mode? _lastAppliedMode = null;

        
        public AppTheme()
        {
            _messenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        }

        public Mode CurrentMode
        {
            get
            {
                var setting = CurrentModeSetting;
                if (setting == ModeSetting.Auto && _delegate != null)
                    return _delegate.SystemColorMode;
                else if (setting == ModeSetting.Dark)
                    return Mode.Dark;
                else
                    return Mode.Light;
            }
        }

        public ModeSetting CurrentModeSetting
        {
            get => (ModeSetting)Preferences.Get(nameof(CurrentModeSetting), (int)ModeSetting.Auto);
            set
            {
                if (CurrentModeSetting != value)
                {
                    Preferences.Set(nameof(CurrentModeSetting), (int)value);
                    ApplyMode(CurrentMode);
                }
            }
        }

        public void SetDelegate(IAppThemeDelegate colorModeDelegate)
        {
            if (_delegate != null)
                _delegate.SystemColorModeChanged -= Delegate_SystemModeChanged;

            _delegate = colorModeDelegate;

            _lastAppliedMode = null;
            ApplyMode(CurrentMode);

            _delegate.SystemColorModeChanged += Delegate_SystemModeChanged;
        }

        void Delegate_SystemModeChanged(Mode newMode)
        {
            if (CurrentModeSetting == ModeSetting.Auto)
            {
                Dlog.Info("Needs new app color mode");
                ApplyMode(newMode);
            }
        }

        void ApplyMode(Mode newMode)
        {
            if (_lastAppliedMode != newMode)
            {
                Dlog.Info("Applying app color mode: " + newMode);
                //if (newMode == Mode.Dark)
                //    ApplyDarkMode();
                //else
                //    ApplyLightMode();

                _messenger.Publish(new AppThemeChangedMessage(this, newMode));
            }
        }
    }

    public class AppThemeChangedMessage : MvxMessage
    {
        public AppThemeChangedMessage(AppTheme appTheme, AppTheme.Mode mode) : base(appTheme) => Mode = mode;
        public AppTheme.Mode Mode { get; }
    }
}
