using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace ComponentLibrary;

public class H1() : Header(ResourceKeys.FontSize.H1);

public class H2() : Header(ResourceKeys.FontSize.H2);

public class H3() : Header(ResourceKeys.FontSize.H3);

public class H4() : Header(ResourceKeys.FontSize.H4);

public class H5() : Header(ResourceKeys.FontSize.H5);

public class Header : TextBlock {
    private readonly string _fontSizeKey;

    public Header(string fontSizeKey) {
        _fontSizeKey = fontSizeKey;
        var thing = ResourceKeys.Button.FontSize.XLarge;
        FontWeight = FontWeight.Bold;
    }

    protected override Type StyleKeyOverride => typeof(TextBlock);

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
        base.OnAttachedToVisualTree(e);

        if(this.TryFindResource(_fontSizeKey, out var value))
            FontSize = (double)value!;
        else
            FontSize = 4;
    }
}
