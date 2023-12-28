using System;
using ReactiveUI;

namespace Client.ViewModels;

public class MainViewModel : ViewModelBase {

    private ViewModelBase _contentViewModel;

    public MainViewModel() {
        _contentViewModel = new LoginViewModel();
        Console.WriteLine("MainWindowViewModel");
    }

    public LoginViewModel LoginView { get; }
    public RegisterViewModel RegisterView { get; }

    public ViewModelBase ContentViewModel {
        get {
            Console.WriteLine("ContentViewModel");
            return _contentViewModel;
        }
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
}