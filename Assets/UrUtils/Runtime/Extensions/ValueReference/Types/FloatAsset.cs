using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Float")]
    public class FloatAsset : ValueAsset<float> {
        public FloatAsset() { }
        public FloatAsset(float value) : base(value) { }
    }

    [Serializable]
    public class FloatReference : ValueReference<float, FloatAsset> { }
}
