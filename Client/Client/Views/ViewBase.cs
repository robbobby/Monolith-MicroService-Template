using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using Client.ViewModels;
using Client.Views.Application;
using Client.Views.Application.Ticket;

namespace Client.Views;

public class ViewBase(ViewModelBase viewModelBase) : UserControl {
    public virtual bool PersistData => true;
    public ViewModelBase ViewModel => viewModelBase;
    public ViewTemplateType ViewTemplate { get; set; } = ViewTemplateType.Auth;
}

public class ModalViewBase(ViewModelBase viewModelBase) : ViewBase(viewModelBase), IModalContentControl {
    public virtual double ModalHeight { get; set; } = 400;
    public virtual double ModalWidth { get; set; } = 400;
    public virtual bool CanResize { get; set; } = false;
    
    protected void CloseModal(object sender, RoutedEventArgs e)
    {
        var window = this.FindAncestorOfType<Window>();
        window?.Close();
    }
}

public enum ViewTemplateType {
    Auth,
    Application,
    Settings
}

public class ApplicationViewBase<T> : ViewBase where T : ViewModelBase {
    public new T ViewModel;

    protected ApplicationViewBase(T viewModelBase) : base(viewModelBase) {
        ViewTemplate = ViewTemplateType.Application;
        ViewModel = viewModelBase;
    }
}

public class SettingsViewBase : ViewBase {
    public SettingsViewBase(ViewModelBase viewModelBase) : base(viewModelBase) {
        ViewTemplate = ViewTemplateType.Settings;
    }
}
