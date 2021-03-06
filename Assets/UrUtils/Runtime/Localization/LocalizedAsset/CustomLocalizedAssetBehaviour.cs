// This file is copied from Unity's LocalizedAssetBehaviour to insert Odin's [DrawWithUnity] attribute
#if URUTILS_UNITY_LOCALIZATION
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif // ODIN_INSPECTOR
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

namespace UrUtils.Localization.LocalizedAsset
{
    /// <summary>
    /// Abstract class that can be inherited from to create a general purpose Localized Asset Component.
    /// This Component handles the Localization of the asset and calls <see cref="UpdateAsset(TObject)"/>
    /// whenever a new Localized Asset is ready.
    /// </summary>
    /// <example>
    /// This example shows how the [Font](https://docs.unity3d.com/ScriptReference/Font.html) asset of a [UGUI Text Component](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/api/UnityEngine.UI.Text.html) could be localized.
    /// ![](../manual/images/scripting/LocalizedFontComponent.png)
    /// <code source="../../DocCodeSamples.Tests/LocalizedFontComponent.cs" region="sample-code"/>
    /// </example>
    /// <typeparam name="TObject">The type of Asset to be Localized. Must inherit from [UnityEngine.Object](https://docs.unity3d.com/ScriptReference/Object.html)</typeparam>
    /// <typeparam name="TReference">The **Serializable** LocalizedAsset class. This will be used for the <see cref="AssetReference"/> property.</typeparam>
    [ExecuteAlways]
    public abstract class CustomLocalizedAssetBehaviour<TObject, TReference> : LocalizedMonoBehaviour
        where TObject : Object
        where TReference : LocalizedAsset<TObject>, new()
    {
        [SerializeField]
#if ODIN_INSPECTOR
        [DrawWithUnity]
#endif
        TReference LocalizedAssetReference = new TReference();

        /// <summary>
        /// Reference to the Table and Entry which will be used to identify the asset being localized.
        /// </summary>
        public TReference AssetReference
        {
            get => LocalizedAssetReference;
            set
            {
                ClearChangeHandler();
                LocalizedAssetReference = value;

                if (isActiveAndEnabled)
                    RegisterChangeHandler();
            }
        }

        protected virtual void OnEnable() => RegisterChangeHandler();

        protected virtual void OnDisable() => ClearChangeHandler();

        void OnDestroy() => ClearChangeHandler();

        void OnValidate()
        {
            // AssetReference.ForceUpdate();
        }

        internal virtual void RegisterChangeHandler() => AssetReference.AssetChanged += UpdateAsset;

        internal virtual void ClearChangeHandler() => AssetReference.AssetChanged -= UpdateAsset;

        /// <summary>
        /// Called when <see cref="AssetReference"/> has been loaded. This will occur when the game first starts after
        /// <see cref="LocalizationSettings.InitializationOperation"/> has completed and whenever
        /// the <see cref="LocalizationSettings.SelectedLocale"/> is changed.
        /// </summary>
        /// <param name="localizedAsset"></param>
        protected abstract void UpdateAsset(TObject localizedAsset);
    }

    /// <summary>
    /// A version of <see cref="LocalizedAssetBehaviour{TObject, TReference}"/> which also includes a [UnityEvent](https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html) with the localized asset.
    /// Using the <see cref="OnUpdateAsset"/> event it is possible to Localize Components without writing scripts specific to the Component that can be configured in the Inspector.
    /// </summary>
    /// <example>
    /// This example shows how a [Font](https://docs.unity3d.com/ScriptReference/Font.html) asset could be localized.
    /// ![](../manual/images/LocalizedFontEventComponent.png)
    /// <code source="../../DocCodeSamples.Tests/LocalizedFontEventComponent.cs" region="sample-code"/>
    /// </example>
    /// <typeparam name="TObject">The type of Asset to be Localized. Must inherit from [UnityEngine.Object](https://docs.unity3d.com/ScriptReference/Object.html)</typeparam>
    /// <typeparam name="TReference">The Serializable LocalizedAsset class. This will be used for the <see cref="LocalizedAssetBehaviour{TObject, TReference}.AssetReference"/> property.</typeparam>
    /// <typeparam name="TEvent">The Serializable [UnityEvent](https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html) that should be called when the asset is loaded.</typeparam>
    public class CustomLocalizedAssetEvent<TObject, TReference, TEvent> : CustomLocalizedAssetBehaviour<TObject, TReference>
        where TObject : Object
        where TReference : LocalizedAsset<TObject>, new()
        where TEvent : UnityEvent<TObject>, new()
    {
        [SerializeField]
        TEvent UpdateAssetEvent = new TEvent();

        /// <summary>
        /// Unity Event that is invoked when the localized asset is updated.
        /// </summary>
        public TEvent OnUpdateAsset
        {
            get => UpdateAssetEvent;
            set => UpdateAssetEvent = value;
        }

        /// <inheritdoc/>
        protected override void UpdateAsset(TObject localizedAsset)
        {
            #if UNITY_EDITOR
            bool isPlayingOrWillEnterPlaymode =
                UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode || Application.isPlaying;
            if (!isPlayingOrWillEnterPlaymode)
            {
                if (AssetReference.IsEmpty)
                {
                    Editor_UnregisterKnownDrivenProperties(OnUpdateAsset);
                    return;
                }

                Editor_RegisterKnownDrivenProperties(OnUpdateAsset);
                CallUpdateAsset(localizedAsset);
                Editor_RefreshEventObjects(OnUpdateAsset);
            }
            else
            #endif
            {
                CallUpdateAsset(localizedAsset);
            }
        }


        // Override this to use update value
        protected virtual void CallUpdateAsset(TObject value)
        {
            OnUpdateAsset?.Invoke(value);
        }
    }
}
#endif // URUTILS_UNITY_LOCALIZATION
