using UnityEngine;

namespace UrUtils.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Change alpha value of the color
        /// </summary>
        /// <param name="color">Input color</param>
        /// <param name="alpha">New alpha value</param>
        /// <returns>Color with changed alpha value</returns>
        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}
