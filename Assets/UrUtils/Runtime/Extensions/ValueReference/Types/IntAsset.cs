using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu]
    public class IntAsset : ValueAsset<int> { }

    [Serializable]
    public class IntReference : ValueReference<int, IntAsset> { }
}
