using UnityEngine;

namespace UrUtils.Extensions.ValueReference
{
    public abstract class ValueAsset<T> : ScriptableObject
    {
        public T Value => StoredValue;

        [SerializeField]
        T StoredValue = default;

        protected ValueAsset() { }

        protected ValueAsset(T value)
        {
            StoredValue = value;
        }
    }

    public static class ValueAssetConstants
    {
        public const string MenuItemRoot = "Value Asset/";
    }
}
