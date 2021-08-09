using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Integer")]
    public class IntAsset : ValueAsset<int> { }

    [Serializable]
    public class IntReference : ValueReference<int, IntAsset> { }
}
