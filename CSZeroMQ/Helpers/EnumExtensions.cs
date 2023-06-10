namespace CSZeroMQ.Helpers;

public static class EnumExtensions
{
    public static T[]? FindAttributes<T>(this Enum e) where T: Attribute
    {
        var field = e.GetType().GetField(e.ToString());
        if (field == null)
            throw new ArgumentException("Value does not a field", nameof(field));
        var attrs = field.GetCustomAttributes(typeof(T), false);
        if (attrs.Length == 0)
            return null;
        return (T[]) attrs;
    }
    public static T? FindAttribute<T>(this Enum e) where T: Attribute
    {
        var field = e.GetType().GetField(e.ToString());
        if (field == null)
            throw new ArgumentException("Value does not a field", nameof(field));
        var attrs = field.GetCustomAttributes(typeof(T), false);
        if (attrs.Length == 0)
            return null;
        return (T) attrs[0];
    }
}