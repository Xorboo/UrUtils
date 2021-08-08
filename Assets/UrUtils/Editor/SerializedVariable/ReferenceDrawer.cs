using UnityEditor;
using UnityEngine;
using UrUtils.Extensions.SerializedVariable;

namespace UrUtils.SerializedVariable
{
    [CustomPropertyDrawer(typeof(StringHolder))]
    public class StringHolderDrawer : ReferenceDrawer { }

    [CustomPropertyDrawer(typeof(FloatHolder))]
    public class FloatHolderDrawer : ReferenceDrawer { }

    [CustomPropertyDrawer(typeof(IntHolder))]
    public class IntHolderDrawer : ReferenceDrawer { }

    [CustomPropertyDrawer(typeof(AssetReferenceHolder))]
    public class AssetReferenceHolderDrawer : ReferenceDrawer { }

    public class ReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        readonly string[] PopupOptions = {"Use Constant", "Use Variable"};

        /// <summary> Cached style to use to draw the popup button. </summary>
        GUIStyle PopupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PopupStyle ??= new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
            {
                imagePosition = ImagePosition.ImageOnly
            };

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty useConstant = property.FindPropertyRelative("_useConstant");
            SerializedProperty constantValue = property.FindPropertyRelative("_constant");
            SerializedProperty variable = property.FindPropertyRelative("_variable");

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += PopupStyle.margin.top;
            buttonRect.width = PopupStyle.fixedWidth + PopupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, PopupOptions, PopupStyle);

            useConstant.boolValue = result == 0;

            EditorGUI.PropertyField(position, useConstant.boolValue ? constantValue : variable, GUIContent.none);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
