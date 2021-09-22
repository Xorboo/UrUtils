using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Integer")]
    public class IntAsset : ValueAsset<int> {
        public IntAsset() { }
        public IntAsset(int value) : base(value) { }
    }

    [Serializable]
    public class IntReference : ValueReference<int, IntAsset> { }
}
