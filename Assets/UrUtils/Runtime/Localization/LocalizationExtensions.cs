#if URUTILS_UNITY_LOCALIZATION
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

namespace UrUtils.Localization
{
    public static class LocalizationExtensions
    {
        public static void SetLocalizationKey(this LocalizeStringEvent localizeString, string localizationKey)
        {
            localizeString.StringReference.SetReference(localizeString.StringReference.TableReference, localizationKey);
        }

        public static void SetLocalizationKey(this LocalizeStringEvent localizeString, TableEntryReference tableEntryReference)
        {
            localizeString.StringReference.SetReference(localizeString.StringReference.TableReference, tableEntryReference);
        }
    }
}
#endif
