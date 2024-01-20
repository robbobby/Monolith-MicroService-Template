using Avalonia.Controls;
using Client.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Views;

public partial class ModalWindowModel : ViewModelBase {
    [ObservableProperty] private UserControl _modalContent;
}
