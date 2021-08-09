using UnityEngine;

namespace UrUtils.Extensions.ValueReference
{
    public abstract class ValueAsset<T> : ScriptableObject
    {
        public T Value => StoredValue;

        [SerializeField]
        T StoredValue = default;
    }

#if UNITY_EDITOR
    public static class ValueAssetConstants
    {
        public const string MenuItemRoot = "Value Asset/";
    }
#endif
}
