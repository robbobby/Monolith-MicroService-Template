using System;
using ReactiveUI;

namespace Client.ViewModels {
    public class MainWindowViewModel : ViewModelBase {
        private ViewModelBase _contentViewModel;

        public MainWindowViewModel() {
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
}