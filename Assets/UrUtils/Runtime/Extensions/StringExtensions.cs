namespace UrUtils.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string @this, bool trimSpaces = false)
        {
            return string.IsNullOrEmpty(trimSpaces ? @this.Trim() : @this);
        }

        public static bool IsNullOrWhiteSpace(this string @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }
    }
}
