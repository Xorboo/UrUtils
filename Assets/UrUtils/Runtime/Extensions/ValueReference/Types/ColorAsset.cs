using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Color")]
    public class ColorAsset : ValueAsset<Color> {
        public ColorAsset() { }
        public ColorAsset(Color value) : base(value) { }
    }

    [Serializable]
    public class ColorReference : ValueReference<Color, ColorAsset> { }
}
