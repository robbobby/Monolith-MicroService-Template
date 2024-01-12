using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Client.Models;

public class BoolToVisibilityConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var isVisible = value is bool b && b;
        return isVisible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return value is bool b && b;
    }
}