using Client.ViewModels;
using FluentAvalonia.UI.Windowing;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Views {
    public partial class MainWindow : AppWindow {
        public MainWindow() : this(App.Services.GetRequiredService<MainWindowViewModel>(), App.Services.GetRequiredService<MainView>()) { }

        public MainWindow(MainWindowViewModel viewModel, MainView content) {
            Content = content;
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}