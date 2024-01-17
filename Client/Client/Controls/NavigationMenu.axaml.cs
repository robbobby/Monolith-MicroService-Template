using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Client.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Controls;

public partial class NavigationMenu : UserControl {
    public static readonly StyledProperty<ToggleMenuModel> ToggleMenuDataContextProperty =
        AvaloniaProperty.Register<StyledElement, ToggleMenuModel>(
            nameof(ToggleMenuDataContext),
            defaultBindingMode: BindingMode.TwoWay);

    public NavigationMenu() {
        InitializeComponent();
    }

    public ToggleMenuModel ToggleMenuDataContext {
        get => GetValue(ToggleMenuDataContextProperty);
        set => SetValue(ToggleMenuDataContextProperty, value);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnAttachedToVisualTree(e);

        var router = App.Services.GetRequiredService<Router>();
        var viewModel = new NavigationMenuModel(router, ToggleMenuDataContext);
        DataContext = viewModel;
    }
}
