using Avalonia.Styling;

namespace Avalonia.Controls;

public static class IResourceNodeExtensions {
    public static T GetResource<T>(this IResourceNode node, string key, ThemeVariant theme) {
        node.TryGetResource(key, null, out var value);
        return (T)value;
    }
}
