using UnityEngine;

namespace UrUtils.Extensions.ValueReference
{
    public abstract class ValueAsset<T> : ScriptableObject
    {
        public T Value => StoredValue;

        [SerializeField] T StoredValue = default;
    }
}
