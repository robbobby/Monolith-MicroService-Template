using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ComponentLibrary;

public partial class Badge : UserControl, INotifyPropertyChanged {
    public static readonly DirectProperty<Badge, string> BadgeNumberProperty =
        AvaloniaProperty.RegisterDirect<Badge, string>(
            nameof(BadgeNumber),
            o => o.BadgeNumber,
            (o, v) => {
                o.BadgeNumber = v;
                o.OnPropertyChanged(nameof(BadgeNumber));
            });

    private string _badgeNumber;

    public Badge() {
        InitializeComponent();
        DataContext = this;
    }

    public string BadgeNumber {
        get => _badgeNumber;
        set => SetAndRaise(BadgeNumberProperty, ref _badgeNumber, value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    protected virtual void OnPropertyChanged(string propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
