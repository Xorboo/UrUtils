using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Boolean")]
    public class BooleanAsset : ValueAsset<bool> { }

    [Serializable]
    public class BooleanReference : ValueReference<bool, BooleanAsset> { }
}
