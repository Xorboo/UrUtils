using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Boolean")]
    public class BooleanAsset : ValueAsset<bool> {
        public BooleanAsset() { }
        public BooleanAsset(bool value) : base(value) { }
    }

    [Serializable]
    public class BooleanReference : ValueReference<bool, BooleanAsset> { }
}
