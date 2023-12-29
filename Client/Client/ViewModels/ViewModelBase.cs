using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public class ViewModelBase : ObservableObject {
    public bool PersistData { get; set; } = true;
}