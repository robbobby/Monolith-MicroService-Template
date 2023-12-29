using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels {
    public partial class MainWindowViewModel : ViewModelBase {
        [ObservableProperty] private static ViewModelBase _contentViewModel;

    }
}