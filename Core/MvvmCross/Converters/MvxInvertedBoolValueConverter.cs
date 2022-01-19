using MvvmCross.Converters;
using System;
using System.Globalization;

namespace Core.MvvmCross.Converters
{
    public class MvxInvertedBoolValueConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type? targetType, object? parameter, CultureInfo? culture) => !value;

        protected override bool ConvertBack(bool value, Type? targetType, object? parameter, CultureInfo? culture) => !value;
    }
}
