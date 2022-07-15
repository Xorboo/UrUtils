// This file is copied from Unity's LocalizeSpriteEvent to insert Odin's [DrawWithUnity] attribute (inside CustomLocalizedAssetBehaviour)
#if URUTILS_UNITY_LOCALIZATION
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Events;
using UnityEngine.Localization.Settings;

namespace UrUtils.Localization.LocalizedAsset
{
    /// <summary>
    /// Component that can be used to Localize a [Sprite](https://docs.unity3d.com/ScriptReference/Sprite.html) asset.
    /// Provides an update event <see cref="LocalizedAssetEvent{TObject,TReference,TEvent}.OnUpdateAsset"/> that can be used to automatically
    /// update the Sprite whenever the <see cref="LocalizationSettings.SelectedLocale"/> or <see cref="LocalizedAssetBehaviour{TObject, TReference}.AssetReference"/> changes.
    /// </summary>
    /// <example>
    /// The example show how it is possible to switch between different Localized Sprites.
    /// ![](../manual/images/scripting/LocalizedSpriteChanger.png)
    /// <code source="../../DocCodeSamples.Tests/LocalizedSpriteChanger.cs"/>
    /// </example>
    /// <remarks>
    /// This component can also be added through the **Localize** menu item in the [Image](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-Image.html) context menu.
    /// Adding it this way will also automatically configure the Update Asset events to update the [Image](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/script-Image.html).
    /// </remarks>
    [AddComponentMenu("Localization/Asset/[Custom] Localize Sprite Event")]
    public class CustomLocalizeSpriteEvent : CustomLocalizedAssetEvent<Sprite, LocalizedSprite, UnityEventSprite>
    {
    }
}
#endif // URUTILS_UNITY_LOCALIZATION
