using Avalonia.Media;
using Avalonia.Styling;

namespace Avalonia.Controls;

public class H1TextBlock : TextBlock, IStyleable {
    public H1TextBlock() {
        FontSize = 32; // equivalent to <H1> in HTML
        FontWeight = FontWeight.Bold;
    }

    Type IStyleable.StyleKey => typeof(TextBlock);
}
