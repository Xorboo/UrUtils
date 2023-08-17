using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "String")]
    public class StringAsset : ValueAsset<string>
    {
        public StringAsset() { }
        public StringAsset(string value) : base(value) { }
    }

    [Serializable]
    public class StringReference : ValueReference<string, StringAsset>
    {
        public StringReference() { }
        public StringReference(string value) : base(value) { }
    }
}
