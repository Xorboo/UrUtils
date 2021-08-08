using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu]
    public class Vector2Asset : ValueAsset<Vector2> { }

    [Serializable]
    public class Vector2Reference : ValueReference<Vector2, Vector2Asset> { }
}
