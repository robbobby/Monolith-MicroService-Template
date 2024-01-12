using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Client.Models;

public class BooleanConverter : IValueConverter {
    public object True { get; set; }
    public object False { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if(value is bool b)
            return b ? True : False;

        return False;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}