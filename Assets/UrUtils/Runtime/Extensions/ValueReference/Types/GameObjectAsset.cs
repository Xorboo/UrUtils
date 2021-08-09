using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Game Object")]
    public class GameObjectAsset : ValueAsset<GameObject> { }

    [Serializable]
    public class GameObjectReference : ValueReference<GameObject, GameObjectAsset> { }
}
