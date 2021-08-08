using UnityEngine;

namespace UrUtils.Extensions.SerializedVariable
{
    public class BaseVariable<T> : ScriptableObject
    {
        public T Value => _value;

        [SerializeField] private T _value = default;
    }
}