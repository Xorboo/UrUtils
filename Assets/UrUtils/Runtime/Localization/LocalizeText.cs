#if URUTILS_UNITY_LOCALIZATION && URUTILS_TM_PRO
using TMPro;
using UnityEngine;

namespace UrUtils.Localization
{
    /// <summary>
    /// Localize TMP_Text attached to the same component
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeText : CustomLocalizeStringEvent
    {
        public TMP_Text Text
        {
            get
            {
                if (TextReference == null)
                    TextReference = GetComponent<TMP_Text>();
                return TextReference;
            }
        }

        TMP_Text TextReference = null;


        protected override void CallUpdateString(string value)
        {
            if (Text != null)
                Text.text = value;

            base.CallUpdateString(value);
        }
    }
}
#endif