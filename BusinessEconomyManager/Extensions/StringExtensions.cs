namespace BusinessEconomyManager.Extensions
{
    public static class StringExtensions
    {
        public static Guid? ToGuid(this string text)
        {
            if (!Guid.TryParse(text, out Guid guid)) return null;
            return guid;
        }

        public static T? ToEnum<T>(this string text) where T : struct
        {
            if (Enum.TryParse<T>(text, out T parsedEnum)) return parsedEnum;
            return null;

        }
    }
}
