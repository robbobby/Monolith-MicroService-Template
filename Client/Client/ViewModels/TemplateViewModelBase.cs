using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class TemplateViewModelBase : ViewModelBase {
    [ObservableProperty] private object _view  = null!;
}