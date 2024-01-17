using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace Avalonia.Controls;

public class H1TextBlock : TextBlock, IStyleable {
    Type IStyleable.StyleKey => typeof(TextBlock);

    public H1TextBlock() {
        this.FontSize = 32; // equivalent to <H1> in HTML
        this.FontWeight = FontWeight.Bold;
    }
}