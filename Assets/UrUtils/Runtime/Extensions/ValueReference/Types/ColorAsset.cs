using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu]
    public class ColorAsset : ValueAsset<Color> { }

    [Serializable]
    public class ColorReference : ValueReference<Color, ColorAsset> { }
}
