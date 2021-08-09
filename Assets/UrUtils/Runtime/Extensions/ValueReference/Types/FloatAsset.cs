using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Float")]
    public class FloatAsset : ValueAsset<float> { }

    [Serializable]
    public class FloatReference : ValueReference<float, FloatAsset> { }
}
