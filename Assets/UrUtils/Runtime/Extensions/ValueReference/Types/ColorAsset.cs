using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Color")]
    public class ColorAsset : ValueAsset<Color> { }

    [Serializable]
    public class ColorReference : ValueReference<Color, ColorAsset> { }
}
