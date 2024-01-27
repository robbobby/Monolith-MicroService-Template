using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Common.Model;
using ComponentLibrary;

namespace Client.Controls.Ticket;

public partial class TicketTypeBadge : UserControl {
    public static readonly StyledProperty<TicketType> TypeProperty = AvaloniaProperty.Register<TicketTypeBadge, TicketType>(nameof(Type));
    public TicketType Type {
        get => (TicketType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
    public TicketTypeBadge() {
        InitializeComponent();
        ToolTipText.Text = Type.ToString();
    }

    protected override void OnInitialized() {
        TypeIcon.Data = GetIcon(Type);
        ToolTipText.Text = Type.ToString();
    }

    private Geometry GetIcon(TicketType type) {
        object? icon;
        _ = type switch {
            TicketType.Bug => Application.Current!.TryGetResource(ResourceKeys.Icon.LaptopRegular, out icon),
            TicketType.Story => Application.Current!.TryGetResource(ResourceKeys.Icon.Backlog, out icon),
            TicketType.Epic => Application.Current!.TryGetResource(ResourceKeys.Icon.Cross, out icon),
            TicketType.Task => Application.Current!.TryGetResource(ResourceKeys.Icon.CalendarRegular, out icon),
            TicketType.SubTask => Application.Current!.TryGetResource(ResourceKeys.Icon.CameraRotate, out icon),
            TicketType.TechnicalDebt => Application.Current!.TryGetResource(ResourceKeys.Icon.AddSquareRegular, out icon),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        return (Geometry)icon!;
    }
}

