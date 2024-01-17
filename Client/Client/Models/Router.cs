using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Client.Views;
using Client.Views.Auth;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Models;

public record HistoryItem(Type Type, ViewBase? State);

public partial class Router : ObservableObject {
    [ObservableProperty] private static ViewBase? _contentView;
    private List<Type> History { get; } = new();
    private int HistoryIndex { get; set; } = -1;

    public void NavigateTo<T>() where T : ViewBase {
        AddHistory(typeof(T));
        ContentView = App.GetViewOrThrow<T>();
    }

    public void NavigateTo(Type type) {
        AddHistory(type);
        ContentView = App.GetViewOrThrow(type);
    }

    private void AddHistory(Type type) {
        if(HistoryIndex != History.Count) History.RemoveRange(HistoryIndex + 1, History.Count - HistoryIndex - 1);

        if(ContentView != null) {
            if(!ContentView.PersistData) {
                foreach (var property in ContentView.ViewModel.GetType().GetProperties())
                    property.SetValue(ContentView.ViewModel, null);
            } else {
                var viewModelType = ContentView.ViewModel.GetType();
                var cultureInfo = CultureInfo.InvariantCulture;

                foreach (var field in viewModelType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(f => Attribute.IsDefined(f, typeof(NonePersistentAttribute)))) {
                    Console.WriteLine($"Clearing {field.Name}");

                    var propertyName = field.Name.TrimStart('_');

                    if(propertyName.Length > 0 && char.IsLower(propertyName[0]))
                        propertyName = cultureInfo.TextInfo.ToTitleCase(propertyName);

                    var property = viewModelType.GetProperty(propertyName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    property?.SetValue(ContentView.ViewModel, null);
                }
            }

            History.Add(type);
            HistoryIndex++;
        }
    }
}
