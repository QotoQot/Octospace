using System;
using System.Globalization;
using System.Resources;
using Core.Logging;
using Core.Resources.Localization;
using MvvmCross;

namespace Core.Resources.Localization
{
    public enum ResX
    {
        Shared,
        Content,
        MainWindow,
        Settings
    }

    public static class TextProvider
    {
        public static string Get(ResX resxFile, string name)
        {
            var resourceManager = resxFile switch
            {
                ResX.Shared => SharedStrings.ResourceManager,
                ResX.Content => ContentStrings.ResourceManager,
                ResX.MainWindow => MainWindowStrings.ResourceManager,
                ResX.Settings => SettingsStrings.ResourceManager,
                _ => throw new ArgumentException($"Incorrect enum value: {resxFile}")
            };

            var text = resourceManager.GetString(name, CultureInfo.CurrentUICulture);

            if (string.IsNullOrEmpty(text))
            {
                Dlog.Error($"Missing text for '{name}' in '{resxFile}'");
                return $"<MISSING {name}>";
            }
            else
                return text;
        }
    }
}
