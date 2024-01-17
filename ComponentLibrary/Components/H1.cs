using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace ComponentLibrary;

public class H1 : TextBlock, IStyleable {
    public H1() {
        FontSize = 32; // equivalent to <H1> in HTML
        FontWeight = FontWeight.Bold;
    }

    Type IStyleable.StyleKey => typeof(TextBlock);
}
