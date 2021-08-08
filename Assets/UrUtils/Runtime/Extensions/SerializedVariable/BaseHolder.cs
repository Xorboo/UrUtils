using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace UrUtils.Extensions.SerializedVariable
{
    [Serializable]
    public class BaseHolder<TBase, TReference> where TReference: BaseVariable<TBase>
    {
        [SerializeField] bool UseConstant = false;
        [SerializeField] TBase Constant = default;
        [SerializeField] TReference Variable = default;

        public BaseHolder() { }

        public BaseHolder(TBase value)
        {
            UseConstant = true;
            Constant = value;
        }

        public TBase Value => UseConstant ? Constant : Variable.Value;

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
