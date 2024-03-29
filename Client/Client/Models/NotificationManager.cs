using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Notification;
using Avalonia.Styling;
using ComponentLibrary;

namespace Client.Models;

public class NotificationManager {
    private readonly IResourceDictionary _resources = Application.Current!.Resources;
    public static INotificationMessageManager Notification { get; } = new NotificationMessageManager();

    public void Success(string message) {
        Success(new NotificationModel { Message = message });
    }

    public void Success(NotificationModel model) {
        _resources.TryGetResource(ResourceKeys.Colour.Success, ThemeVariant.Dark, out var accentColour);
        _resources.TryGetResource(ResourceKeys.Colour.Background.Contrast, ThemeVariant.Dark, out var backgroundColour);
        if(string.IsNullOrEmpty(model.Badge)) model.Badge = "Success";

        var notification = BuildNotification(model, accentColour, backgroundColour);
        notification.Queue();
    }

    public void Info(string message) {
        Info(new NotificationModel { Message = message });
    }

    public void Info(NotificationModel model) {
        _resources.TryGetResource(ResourceKeys.Colour.Info, ThemeVariant.Dark, out var accentColour);
        _resources.TryGetResource(ResourceKeys.Colour.Background.Contrast, ThemeVariant.Dark, out var backgroundColour);
        if(string.IsNullOrEmpty(model.Badge)) model.Badge = "Info";

        var notification = BuildNotification(model, accentColour, backgroundColour);
        notification.Queue();
    }

    public void Warning(string message) {
        Warning(new NotificationModel { Message = message });
    }

    public void Warning(NotificationModel model) {
        _resources.TryGetResource(ResourceKeys.Colour.Warning, ThemeVariant.Dark, out var accentColour);
        _resources.TryGetResource(ResourceKeys.Colour.Background.Contrast, ThemeVariant.Dark, out var backgroundColour);
        if(string.IsNullOrEmpty(model.Badge)) model.Badge = "Warning";

        var notification = BuildNotification(model, accentColour, backgroundColour);
        notification.Queue();
    }

    public void Error(string message) {
        Error(new NotificationModel { Message = message });
    }

    public void Error(NotificationModel model) {
        _resources.TryGetResource(ResourceKeys.Colour.Danger, ThemeVariant.Dark, out var accentColour);
        _resources.TryGetResource(ResourceKeys.Colour.Background.Contrast, ThemeVariant.Dark, out var backgroundColour);
        if(string.IsNullOrEmpty(model.Badge)) model.Badge = "Error";

        var notification = BuildNotification(model, accentColour, backgroundColour);
        notification.Queue();
    }

    private NotificationMessageBuilder BuildNotification(NotificationModel model, object? accentColour,
                                                         object? backgroundColour) {
        var notification = Notification.CreateMessage();
        notification.Animates(true);
        notification.Background(backgroundColour!.ToString()!);
        notification.Accent(accentColour!.ToString()!);
        notification.Dismiss().WithDelay(TimeSpan.FromSeconds(5));
        notification.Dismiss().WithButton("X", button => { });
        notification.HasBadge(model.Badge);

        if(!string.IsNullOrEmpty(model.Header)) notification.HasHeader(model.Header);
        if(!string.IsNullOrEmpty(model.Message)) notification.HasMessage(model.Message);

        return notification;
    }
}

public class NotificationModel {
    public string Message { get; set; }
    public string Header { get; set; }
    public string Badge { get; set; }
}
