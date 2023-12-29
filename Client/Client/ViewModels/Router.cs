using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Client.ViewModels;

public record HistoryItem {
    public Type Type { get; init; } = null!;
    public ViewModelBase? State { get; init; }
}

public abstract class Router {
    private static ViewModelBase? _contentViewModel;
    private static List<Type> History { get; } = new();
    private static List<HistoryItem> HistoryViewModelState { get; } = new();
    private static int HistoryIndex { get; set; } = -1;

    public static event PropertyChangedEventHandler? ViewChange;

    public static ViewModelBase? ContentViewModel {
        get => _contentViewModel;
        set {
            if (_contentViewModel != value) {
                _contentViewModel = value;
                OnRouteChange(nameof(ContentViewModel));
            }
        }
    }

    private static void OnRouteChange(string propertyName) {
        ViewChange?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }

    // public static void GoBack() {
        // if (HistoryIndex == 0) return;
        // HistoryIndex--;
        // var previousState = History[HistoryIndex].State;
        // if (previousState != null) {
            // ContentViewModel = previousState;
            // return;
        // }

        // ContentViewModel = (ViewModelBase)Activator.CreateInstance(History[HistoryIndex].Type)!;
    // }

    // public static void GoForward() {
        // if (HistoryIndex == History.Count - 1) return;
        // HistoryIndex++;
        // var previousState = History[HistoryIndex].State;
        // if (previousState != null) {
            // ContentViewModel = previousState;
            // return;
        // }
    // }

    public static void NavigateTo<T>() where T : ViewModelBase {
        Console.WriteLine($"HistoryIndex: {HistoryIndex}, History.Count: {History.Count}");
        if (HistoryIndex != History.Count) {
            History.RemoveRange(HistoryIndex + 1, History.Count - HistoryIndex - 1);
        }

        if (ContentViewModel != null) {
            if(ContentViewModel.PersistData && HistoryViewModelState.FirstOrDefault(x => x.Type == ContentViewModel.GetType()) == null)
                HistoryViewModelState.Add(new HistoryItem {
                Type = ContentViewModel.GetType(),
                State = ContentViewModel
            });

            History.Add(typeof(T));
            HistoryIndex++;
        }

        Console.WriteLine(HistoryViewModelState.Count);
        if (HistoryIndex != -1) {
            var persistedState = HistoryViewModelState.FirstOrDefault(x => x.Type == typeof(T))?.State;
            if (persistedState != null) {
                ContentViewModel = persistedState;
                return;
            }

            ContentViewModel = (ViewModelBase)Activator.CreateInstance(typeof(T))!;
            return;
        }

        ContentViewModel = (ViewModelBase)Activator.CreateInstance(typeof(T))!;
    }
}