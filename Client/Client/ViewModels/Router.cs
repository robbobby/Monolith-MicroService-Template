using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Client.ViewModels;

public record HistoryItem(Type Type, ViewBase? State);

public abstract class Router {
    private static ViewBase? _contentView;
    private static List<Type> History { get; } = new();
    private static int HistoryIndex { get; set; } = -1;

    public static event PropertyChangedEventHandler? ViewChange;

    public static ViewBase? ContentView {
        get => _contentView;
        set {
            if (_contentView != value) {
                _contentView = value;
                OnRouteChange(nameof(ContentView));
            }
        }
    }

    private static void OnRouteChange(string propertyName) {
        ViewChange?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }

    public static void NavigateTo<T>() where T : ViewBase {
        if (HistoryIndex != History.Count) {
            History.RemoveRange(HistoryIndex + 1, History.Count - HistoryIndex - 1);
        }

        if (ContentView != null) {
            if (!ContentView.PersistData) {
                foreach (var property in ContentView.ViewModel.GetType().GetProperties()) {
                    property.SetValue(ContentView.ViewModel, null);
                }
            } else {
                var viewModelType = ContentView.ViewModel.GetType();
                var cultureInfo = System.Globalization.CultureInfo.InvariantCulture;

                foreach (var field in viewModelType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(f => Attribute.IsDefined(f, typeof(NonePersistentAttribute)))) {
                    Console.WriteLine($"Clearing {field.Name}");

                    var propertyName = field.Name.TrimStart('_');

                    if (propertyName.Length > 0 && char.IsLower(propertyName[0])) {
                        propertyName = cultureInfo.TextInfo.ToTitleCase(propertyName);
                    }

                    var property = viewModelType.GetProperty(propertyName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    property?.SetValue(ContentView.ViewModel, null);
                }
            }

            History.Add(typeof(T));
            HistoryIndex++;
        }

        ContentView = App.Services.GetService<T>();
    }
}