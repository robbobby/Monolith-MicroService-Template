namespace Core.Extensions;

public static class StringExtensions {
    public static Guid? ParseGuid(this string guidString) {
        if(guidString == null) return null;

        if(Guid.TryParse(guidString, out var guid)) return guid;
        return null;
    }
}
