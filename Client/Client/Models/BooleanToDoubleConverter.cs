using System;
using Avalonia.Markup.Xaml;

namespace Client.Models;

public class BooleanToDoubleConverter : MarkupExtension {
    public double TrueValue { get; set; } = 50;
    public double FalseValue { get; set; } = 0;

    public override object ProvideValue(IServiceProvider serviceProvider) {
        return new Func<bool, double>(value => {
            return value ? TrueValue : FalseValue;
        });
    }
}