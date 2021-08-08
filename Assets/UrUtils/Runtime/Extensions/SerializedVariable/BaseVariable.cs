using UnityEngine;

namespace UrUtils.Extensions.SerializedVariable
{
    public class BaseVariable<T> : ScriptableObject
    {
        public T Value => StoredValue;

        [SerializeField] T StoredValue = default;
    }
}