using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class ViewModelBase : ObservableObject {
    [ObservableProperty] private bool _isLoading;
    public bool PersistData { get; set; } = true;
}
