using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace ComponentLibrary;

public class H1 : TextBlock, IStyleable {
    Type IStyleable.StyleKey => typeof(TextBlock);

    public H1() {
        this.FontSize = 32; // equivalent to <H1> in HTML
        this.FontWeight = FontWeight.Bold;
    }
}