#if URUTILS_UNITY_LOCALIZATION
using UnityEngine;
using UnityEngine.UI;

namespace UrUtils.Localization.LocalizedAsset
{
    /// <summary>
    /// Localize TMP_Text attached to the same component
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class LocalizeImage : CustomLocalizeSpriteEvent
    {
        public Image Image
        {
            get
            {
                if (ImageReference == null)
                    ImageReference = GetComponent<Image>();
                return ImageReference;
            }
        }

        Image ImageReference = null;


        protected override void CallUpdateAsset(Sprite value)
        {
            if (Image != null)
                Image.sprite = value;

            base.CallUpdateAsset(value);
        }
    }
}
#endif