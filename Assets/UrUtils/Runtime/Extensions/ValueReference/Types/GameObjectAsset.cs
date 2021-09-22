using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "Game Object")]
    public class GameObjectAsset : ValueAsset<GameObject> {
        public GameObjectAsset() { }
        public GameObjectAsset(GameObject value) : base(value) { }
    }

    [Serializable]
    public class GameObjectReference : ValueReference<GameObject, GameObjectAsset> { }
}
