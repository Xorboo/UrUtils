namespace UrUtils.Misc
{
    public static class TextFormatting
    {
        public static string BytesToString(double bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (bytes >= 1000 && order < sizes.Length - 1)
            {
                order++;
                bytes /= 1000;
            }

            // Show a single decimal place, and no space.
            return $"{bytes:0.#} {sizes[order]}";
        }
    }
}
