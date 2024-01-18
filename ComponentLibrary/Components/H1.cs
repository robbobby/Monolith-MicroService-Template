using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace ComponentLibrary {
    public class H1() : Header("FontSize.H1");

    public class H2() : Header("FontSize.H2");

    public class H3() : Header("FontSize.H3");

    public class H4() : Header("FontSize.H4");

    public class H5() : Header("FontSize.H5");

    public class Header : TextBlock {
        private readonly string _fontSizeKey;

        public Header(string fontSizeKey) {
            _fontSizeKey = fontSizeKey;
            FontWeight = Avalonia.Media.FontWeight.Bold;
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
            base.OnAttachedToVisualTree(e);

            if(this.TryFindResource(_fontSizeKey, out var value)) {
                FontSize = (double)value;
            } else {
                FontSize = 4;
            }
        }

        protected override Type StyleKeyOverride => typeof(TextBlock);
    }
}
