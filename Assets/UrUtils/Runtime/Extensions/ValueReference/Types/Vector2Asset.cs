using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Vector2")]
    public class Vector2Asset : ValueAsset<Vector2> {
        public Vector2Asset() { }
        public Vector2Asset(Vector2 value) : base(value) { }
    }

    [Serializable]
    public class Vector2Reference : ValueReference<Vector2, Vector2Asset> { }
}
