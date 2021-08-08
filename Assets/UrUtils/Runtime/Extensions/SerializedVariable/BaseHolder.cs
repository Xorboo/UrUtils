using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UrUtils.Extensions.SerializedVariable;

namespace UrUtils.Extensions
{
    [Serializable]
    public class BaseHolder<TBase, TReference> where TReference: BaseVariable<TBase>
    {
        [SerializeField] bool _useConstant = false;
        [SerializeField] TBase _constant = default;
        [SerializeField] TReference _variable = default;

        public BaseHolder() { }

        public BaseHolder(TBase value)
        {
            _useConstant = true;
            _constant = value;
        }

        public TBase Value => _useConstant ? _constant : _variable.Value;

        public static implicit operator TBase(BaseHolder<TBase, TReference> reference)
        {
            return reference.Value;
        }
    }

    [Serializable]
    public class StringHolder : BaseHolder<string, StringVariable> { }

    [Serializable]
    public class FloatHolder : BaseHolder<float, FloatVariable> { }

    [Serializable]
    public class IntHolder : BaseHolder<int, IntVariable> { }

    [Serializable]
    public class AssetReferenceHolder : BaseHolder<AssetReference, AssetReferenceVariable> { }
}
