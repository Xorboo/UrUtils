using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu]
    public class Vector3Asset : ValueAsset<Vector3> { }

    [Serializable]
    public class Vector3Reference : ValueReference<Vector3, Vector3Asset> { }
}
