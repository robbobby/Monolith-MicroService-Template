using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Client.ViewModels;

namespace Client;

public class ViewLocator : IDataTemplate {
    public Control? Build(object? data) {
        if(data is null)
            return null;

        var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if(type != null) return (Control)App.Services.GetService(type)!;

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data) {
        return data is ViewModelBase;
    }
}
