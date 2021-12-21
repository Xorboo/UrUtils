#if URUTILS_UNITY_LOCALIZATION
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace UrUtils.Localization
{
    public class LanguageSwitch : MonoBehaviour
    {
        [SerializeField]
        LocaleIdentifier LocaleId;


        public void SetLanguage()
        {
            var locale = GetLocale();
            if (locale == null)
            {
                return;
            }

            Debug.Log($"Changing locale to [{locale.LocaleName}]");
            LocalizationSettings.SelectedLocale = locale;
        }

        Locale GetLocale()
        {
            var locale = LocalizationSettings.AvailableLocales.GetLocale(LocaleId);
            if (locale == null)
                Debug.LogError($"Can't find locale [{LocaleId}] on {gameObject.name}");

            return locale;
        }
    }
}
#endif
