using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Common.Model;
using ComponentLibrary;

namespace Client.Models.Converters;

public class TypeToIcon : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        object icon = null;
        if(value is TicketType ticketType) {
            switch (ticketType) {
                case TicketType.Bug:
                    Application.Current!.TryGetResource(ResourceKeys.Icon.Backlog, out icon!);
                    break;
                case TicketType.Story:
                    Application.Current!.TryGetResource(ResourceKeys.Icon.LaptopRegular, out icon!);
                    break;
                case TicketType.Epic:
                    Application.Current!.TryGetResource(ResourceKeys.Icon.LaptopRegular, out icon!);
                    break;
                case TicketType.Task:
                    Application.Current!.TryGetResource(ResourceKeys.Icon.LaptopRegular, out icon!);
                    break;
                case TicketType.SubTask:
                    Application.Current!.TryGetResource(ResourceKeys.Icon.LaptopRegular, out icon!);
                    break;
                case TicketType.TechnicalDebt:
                    Application.Current!.TryGetResource(ResourceKeys.Icon.LaptopRegular, out icon!);
                    break;
                default:
                    return (Application.Current!.Resources[ResourceKeys.Icon.LaptopRegular] as Geometry)!;
            }
        }

        return icon;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
