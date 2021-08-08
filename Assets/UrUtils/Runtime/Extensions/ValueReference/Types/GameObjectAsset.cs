using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu]
    public class GameObjectAsset : ValueAsset<GameObject> { }

    [Serializable]
    public class GameObjectReference : ValueReference<GameObject, GameObjectAsset> { }
}
