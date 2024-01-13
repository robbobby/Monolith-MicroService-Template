using Client.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Views;

public partial class MainWindowViewModel : ViewModelBase {
    [ObservableProperty] private static ViewModelBase _contentViewModel;
}