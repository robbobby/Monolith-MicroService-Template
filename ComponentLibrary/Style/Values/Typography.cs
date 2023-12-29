using Avalonia;

namespace AvaloniaApplication1.Style;

public class Typography
{
    internal static int BaseFontSize { get; set; } = 14;
    public ButtonTypography Button { get; } = new();
    public RadiusTypography Radius { get; } = new();
    public HeaderTypography Header { get; } = new();
    public TextTypography Text { get; } = new();
}

public class HeaderTypography
{
    public double H1 => Typography.BaseFontSize * 2.5;
    public double H2 => Typography.BaseFontSize * 2;
    public double H3 => Typography.BaseFontSize * 1.75;
    public double H4 => Typography.BaseFontSize * 1.5;
    public double H5 => Typography.BaseFontSize * 1.25;
}

public class TextTypography
{
    public double Paragraph => Typography.BaseFontSize; 
    public double Caption => Typography.BaseFontSize * 0.875;
    public double Overline => Typography.BaseFontSize * 0.75;
}

public class ButtonTypography
{
    public int XLarge => Typography.BaseFontSize * 2;
    public double Large => Typography.BaseFontSize * 1.5;
    public double Medium => Typography.BaseFontSize;
    public double Small => Typography.BaseFontSize * 0.875;
}

public class RadiusTypography
{
    private const int BaseRadius = 3;
    public CornerRadius Small => new CornerRadius(BaseRadius);
    public CornerRadius Medium => new CornerRadius(BaseRadius * 2);
    public CornerRadius Large => new CornerRadius(BaseRadius * 4);
}