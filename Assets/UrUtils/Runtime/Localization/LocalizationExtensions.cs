#if URUTILS_UNITY_LOCALIZATION
using UnityEngine.Localization.Components;

namespace UrUtils.Localization
{
    public static class LocalizationExtensions
    {
        public static void SetLocalizationKey(this LocalizeStringEvent localizeString, string localizationKey)
        {
            localizeString.StringReference.SetReference(localizeString.StringReference.TableReference, localizationKey);
        }
    }
}
#endif