using UrUtils.Extensions.ValueReference.Types;

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
            var r = new FloatReference();
            var x = new FloatReference(4.1f);
            return string.IsNullOrWhiteSpace(@this);
        }
    }
}
