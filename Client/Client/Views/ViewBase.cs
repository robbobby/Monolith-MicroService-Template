using Avalonia.Controls;
using Client.ViewModels;

namespace Client.Views;

public class ViewBase(ViewModelBase viewModelBase) : UserControl {
    public virtual bool PersistData => true;
    public ViewModelBase ViewModel => viewModelBase;
    public ViewTemplateType ViewTemplate { get; set; } = ViewTemplateType.Auth;
}

public enum ViewTemplateType {
    Auth,
    Application
}

public class ApplicationViewBase : ViewBase {
    public ApplicationViewBase(ViewModelBase viewModelBase) : base(viewModelBase) {
        ViewTemplate = ViewTemplateType.Application;
    }
}