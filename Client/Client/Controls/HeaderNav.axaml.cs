using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Controls;

public partial class HeaderNav : UserControl {
    public HeaderNav() {
        InitializeComponent();
        DataContext = App.Services.GetService<HeaderNavViewModel>()!;
    }
}