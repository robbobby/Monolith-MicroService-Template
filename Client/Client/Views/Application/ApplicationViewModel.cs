using Avalonia.Notification;
using Client.Models;
using Client.ViewModels;

namespace Client.Views.Application;

public class ApplicationViewModel(INotificationMessageManager Manager, NotificationManager notificationManager)
    : ViewModelBase {
    public void ButtonBaseErrorOnClick() {
        notificationManager.Error(new NotificationModel {
            Header = "This is an error Header",
            Message = "This is an error message"
        });
    }

    public void ButtonBaseWarningOnClick() {
        notificationManager.Warning(new NotificationModel {
            Header = "This is a warning Header",
            Message = "This is a warning message"
        });
    }

    public void ButtonBaseInfoOnClick() {
        notificationManager.Info(new NotificationModel {
            Header = "This is an info Header",
            Message = "This is an info message"
        });
    }

    public void ButtonBaseInfoDelayOnClick() {
        notificationManager.Success(new NotificationModel {
            Header = "This is an info Header",
            Message = "This is an info message"
        });
    }
}
