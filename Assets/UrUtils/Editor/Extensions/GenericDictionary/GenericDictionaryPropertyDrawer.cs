using UnityEditor;
using UnityEngine;
using UrUtils.Inspector.GenericDictionary;

namespace UrUtils.Extensions.GenericDictionary
{
    /// <summary>
    /// Draws the generic dictionary a bit nicer than Unity would natively (not as many expand-arrows
    /// and better spacing between KeyValue pairs). Also renders a warning-box if there are duplicate
    /// keys in the dictionary.
    /// </summary>
    [CustomPropertyDrawer(typeof(GenericDictionary<,>))]
    public class GenericDictionaryPropertyDrawer : PropertyDrawer
    {
        static readonly float LineHeight = EditorGUIUtility.singleLineHeight;
        static readonly float VertSpace = EditorGUIUtility.standardVerticalSpacing;
        static readonly float CombinedPadding = LineHeight + VertSpace;

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            // Render list header and expand arrow.
            var list = property.FindPropertyRelative("list");
            var headerPos = new Rect(LineHeight, pos.y, pos.width, LineHeight);
            string fieldName = ObjectNames.NicifyVariableName(fieldInfo.Name);
            EditorGUI.PropertyField(headerPos, list, new GUIContent(fieldName));

            // Render list if expanded.
            var currentPos = new Rect(LineHeight, pos.y, pos.width, LineHeight);
            if (list.isExpanded)
            {
                // Render list size first.
                list.NextVisible(true);
                EditorGUI.indentLevel++;
                currentPos = new Rect(headerPos.x, headerPos.y + CombinedPadding, pos.width, LineHeight);
                EditorGUI.PropertyField(currentPos, list, new GUIContent("Size"));

                // Render list content.
                currentPos.y += VertSpace;
                while (true)
                {
                    if (list.name == "Key" || list.name == "Value")
                    {
                        // Render key or value.
                        var entryPos = new Rect(currentPos.x, currentPos.y + CombinedPadding, pos.width, LineHeight);
                        EditorGUI.PropertyField(entryPos, list, new GUIContent(list.name));
                        currentPos.y += CombinedPadding;

                        // Add spacing after each key value pair.
                        if (list.name == "Value")
                        {
                            currentPos.y += VertSpace;
                        }
                    }
                    if (!list.NextVisible(true)) break;
                }
            }

            // If there are key collisions render warning box.
            bool keyCollision = property.FindPropertyRelative("keyCollision").boolValue;
            if (keyCollision)
            {
                var entryPos = new Rect(LineHeight, currentPos.y + CombinedPadding, pos.width, LineHeight * 2f);
                EditorGUI.HelpBox(entryPos, "There are duplicate keys in the dictionary." +
                                            " Duplicate keys will not be serialized.", MessageType.Warning);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totHeight = 0f;

            // If there is a key collision account for height of warning box.
            bool keyCollision = property.FindPropertyRelative("keyCollision").boolValue;
            if (keyCollision)
            {
                totHeight += LineHeight * 2f + VertSpace;
            }

            // Return height of KeyValue list (take into account if list is expanded or not).
            var listProp = property.FindPropertyRelative("list");
            if (listProp.isExpanded)
            {
                listProp.NextVisible(true);
                int listSize = listProp.intValue;
                totHeight += listSize * 2f * CombinedPadding + CombinedPadding * 2f + listSize * VertSpace;
                return totHeight;
            }
            else
            {
                return totHeight + LineHeight;
            }
        }
    }
}