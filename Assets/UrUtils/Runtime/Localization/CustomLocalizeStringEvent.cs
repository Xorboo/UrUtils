#if URUTILS_UNITY_LOCALIZATION
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Events;
using UnityEngine.Localization.Tables;

namespace UrUtils.Localization
{
    /// <summary>
    /// Duplicate for LocalizeStringEvent to have more control over it
    /// </summary>
    public class CustomLocalizeStringEvent : LocalizedMonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        string ElementHint;
#endif

        [SerializeField, DrawWithUnity]
        LocalizedString LocalizedString = new LocalizedString();

        [SerializeField]
        UnityEventString m_UpdateString = new UnityEventString();


        /// <summary>
        /// References the <see cref="StringTable"/> and <see cref="StringTableEntry"/> of the localized string.
        /// </summary>
        public LocalizedString StringReference
        {
            get => LocalizedString;
            set
            {
                // Unsubscribe from the old string reference.
                ClearChangeHandler();

                LocalizedString = value;

                if (isActiveAndEnabled)
                    RegisterChangeHandler();
            }
        }

        /// <summary>
        /// Event that will be sent when the localized string is available.
        /// </summary>
        public UnityEventString OnUpdateString
        {
            get => m_UpdateString;
            set => m_UpdateString = value;
        }

        /// <summary>
        /// Forces the string to be regenerated, such as when the string formatting argument values have changed.
        /// </summary>
        public void RefreshString()
        {
            StringReference.RefreshString();
        }

        /// <summary>
        /// Set localization key, keeping the same table
        /// </summary>
        public void SetLocalizationKey(string localizationKey)
        {
            StringReference.SetReference(StringReference.TableReference, localizationKey);
        }

        /// <summary>
        /// Set localization key, keeping the same table
        /// </summary>
        public void SetLocalizationKey(TableEntryReference tableEntryReference)
        {
            StringReference.SetReference(StringReference.TableReference, tableEntryReference);
        }


        #region Unity

        protected virtual void OnEnable() => RegisterChangeHandler();

        protected virtual void OnDisable() => ClearChangeHandler();

        void OnDestroy() => ClearChangeHandler();

        void OnValidate() => RefreshString();

        #endregion


        // Override this to use update value
        protected virtual void CallUpdateString(string value)
        {
            OnUpdateString.Invoke(value);
        }


        /// <summary>
        /// Invokes the <see cref="OnUpdateString"/> event.
        /// </summary>
        /// <param name="value"></param>
        void UpdateString(string value)
        {
#if UNITY_EDITOR
            bool isPlayingOrWillEnterPlaymode =
                UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode || Application.isPlaying;
            if (!isPlayingOrWillEnterPlaymode)
            {
                if (StringReference.IsEmpty)
                {
                    Editor_UnregisterKnownDrivenProperties(OnUpdateString);
                    return;
                }

                Editor_RegisterKnownDrivenProperties(OnUpdateString);
                CallUpdateString(value);
                Editor_RefreshEventObjects(OnUpdateString);
            }
            else
#endif
            {
                CallUpdateString(value);
            }
        }

        protected virtual void RegisterChangeHandler()
        {
            StringReference.StringChanged += UpdateString;
        }

        protected virtual void ClearChangeHandler()
        {
            StringReference.StringChanged -= UpdateString;
        }
    }
}
#endif
