#if URUTILS_UNITY_LOCALIZATION && URUTILS_ADDRESSABLES
using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UrUtils.Misc;

namespace UrUtils.Localization
{
    [DefaultExecutionOrder(-10000)]
    public class LocalizationInitializer : Singleton<LocalizationInitializer>
    {
        public event Action OnCompleted;
        public event Action<Locale> OnLocaleChanged;

        public bool IsInitialized { get; private set; }
        public float LocalizationDelay => RecommendedGetDelay;

        [SerializeField]
        LocalizationTableData UiTable = null;

        [SerializeField, Min(0)]
        float RecommendedGetDelay = 1f;


        #region Unity

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(InitializeLocalization());
        }

        void OnEnable()
        {
            LocalizationSettings.Instance.OnSelectedLocaleChanged += LocaleChanged;
        }

        void OnDisable()
        {
            LocalizationSettings.Instance.OnSelectedLocaleChanged -= LocaleChanged;
        }

        #endregion

        public string GetUiLine(string localizationTag)
        {
            return UiTable.GetLine(localizationTag);
        }

        IEnumerator InitializeLocalization()
        {
            yield return LocalizationSettings.InitializationOperation;

            Debug.Log($"Localization initialized");
            IsInitialized = true;
            OnCompleted?.Invoke();
        }

        void LocaleChanged(Locale locale)
        {
            OnLocaleChanged?.Invoke(locale);
        }


        [Serializable]
        class LocalizationTableData
        {
            [SerializeField, Required]
            LocalizedStringTable LocalizedTable = null;

            public string GetLine(string localizationTag)
            {
                var asyncOperation = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(LocalizedTable.TableReference, localizationTag);
#if UNITY_WEBGL
                // Can't use WaitForCompletion() in WebGL
                if (!asyncOperation.IsDone)
                {
                    Debug.LogWarning($"Tried to get tag [{localizationTag}] in WebGL, but operation is not done. Should wait for table to load?");
                    return null;
                }

                return asyncOperation.Result;
#else
                return asyncOperation.WaitForCompletion();
#endif
            }
        }
    }
}
#endif