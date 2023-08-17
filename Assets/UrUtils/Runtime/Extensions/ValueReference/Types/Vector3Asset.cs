using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Vector3")]
    public class Vector3Asset : ValueAsset<Vector3>
    {
        public Vector3Asset() { }
        public Vector3Asset(Vector3 value) : base(value) { }
    }

    [Serializable]
    public class Vector3Reference : ValueReference<Vector3, Vector3Asset>
    {
        public Vector3Reference() { }
        public Vector3Reference(Vector3 value) : base(value) { }
    }
}
