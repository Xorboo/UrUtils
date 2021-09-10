using System.Text;

namespace UrUtils.Extensions
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendNonAlloc(this StringBuilder s, int value)
        {
            if (value == 0)
                return s.Append('0');

            return value > 0
                ? s.AppendIntRecursive(value)
                : s.AppendNegativeIntRecursive(value);
        }

        public static StringBuilder AppendNonAlloc(this StringBuilder s, long value)
        {
            if (value == 0)
                return s.Append('0');

            return value > 0
                ? s.AppendLongRecursive(value)
                : s.AppendNegativeLongRecursive(value);
        }

        static StringBuilder AppendNegativeIntRecursive(this StringBuilder s, int value)
        {
            s.Append('-');
            return s
                .AppendIntRecursive(-(value / 10)) // Handling int.MinValue
                .Append((char)(-(value % 10) + '0'));
        }

        static StringBuilder AppendIntRecursive(this StringBuilder s, int value)
        {
            if (value == 0)
                return s;

            return s
                .AppendIntRecursive(value / 10)
                .Append((char)(value % 10 + '0'));
        }

        static StringBuilder AppendNegativeLongRecursive(this StringBuilder s, long value)
        {
            s.Append('-');
            return s
                .AppendLongRecursive(-(value / 10)) // Handling int.MinValue
                .Append((char)(-(value % 10) + '0'));
        }

        static StringBuilder AppendLongRecursive(this StringBuilder s, long value)
        {
            if (value == 0)
                return s;

            return s
                .AppendLongRecursive(value / 10)
                .Append((char)(value % 10 + '0'));
        }
    }
}
