using System;
using UnityEngine;

namespace UrUtils.Extensions.ValueReference.Types
{
    [CreateAssetMenu(menuName = ValueAssetConstants.MenuItemRoot + "String")]
    public class StringAsset : ValueAsset<string> { }

    [Serializable]
    public class StringReference : ValueReference<string, StringAsset> { }
}
