using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Client.Models.State;
using Client.ViewModels;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace Client.Views.Application;

public partial class BacklogViewModel : ViewModelBase {
    public Interaction<ModalWindow, object?> ShowModal { get; }
    public BacklogViewModel() {
        ShowModal = new Interaction<ModalWindow, object?>();
    }
    
    [RelayCommand]
    public async Task OpenModal() {
        Console.WriteLine("Opening modal");
        var viewModel = new ModalWindowModel {
            ModalContent = App.GetViewOrThrow<UnitCreateView>()
        };
        
        var view = new ModalWindow {
            CanResize = false,
            Width = 300,
            Height = 200,
            DataContext = viewModel
        };
        await ShowModal.Handle(view);
    }
}
