#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace UrUtils.Extensions.ValueReference
{
#if ODIN_INSPECTOR
    [InlineProperty]
    [LabelWidth(80)]
    #endif
    public abstract class ValueReference<TValue, TAsset> where TAsset : ValueAsset<TValue>
    {
        #region Value Selection

        protected enum UseType
        {
                V, // Use Value
                R  // Use Reference
        };

#if ODIN_INSPECTOR
        [HorizontalGroup("Reference", MaxWidth = 30f)]
        [EnumToggleButtons]
        [HideLabel]
        [PropertyTooltip("Choose whether to use locally stored Value of ScriptableObject Reference")]
#endif
        [SerializeField]
        protected UseType Type = UseType.R;

#if ODIN_INSPECTOR
        [ShowIf(nameof(UseValue), Animate = false)]
        [HorizontalGroup("Reference")]
        [HideLabel]
#endif
        [SerializeField]
        protected TValue ValueData = default;

#if ODIN_INSPECTOR
        [HideIf(nameof(UseValue), Animate = false)]
        [HorizontalGroup("Reference")]
        [OnValueChanged(nameof(UpdateAssetReference))]
        [HideLabel]
#endif
        [SerializeField]
        protected TAsset ReferenceData = default;

        #endregion


        #region Inline ScriptableObject Editing

#if ODIN_INSPECTOR
        [HorizontalGroup("Reference", MaxWidth = 15f)]
        [ShowIf(nameof(ShowEditButton), Animate = false)]
        [LabelText("@" + nameof(EditButtonText)), LabelWidth(15f)]
        [PropertyTooltip("Show and edit ScriptableObject")]
        [SerializeField]
        protected bool EditAsset = false;

        [ShowIf(nameof(ShowEditPanel), Animate = false)]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [SerializeField]
        protected TAsset EditableReferenceData = default;
#endif

        #endregion


        #region Getters

        public TValue Value => UseValue || ReferenceData == null ? ValueData : ReferenceData.Value;

        public static implicit operator TValue(ValueReference<TValue, TAsset> valueReference)
        {
            return valueReference.Value;
        }

        #endregion

        #region Editor Helpers

        bool UseValue => Type == UseType.V;

#if ODIN_INSPECTOR
        bool UseReference => !UseValue;

        bool ShowEditButton => ReferenceData != null && UseReference;
        string EditButtonText => EditAsset ? "▲" : "▼";
        bool ShowEditPanel => ShowEditButton && EditAsset;

        void UpdateAssetReference()
        {
            EditableReferenceData = ReferenceData;
        }
#endif

        #endregion
    }
}
