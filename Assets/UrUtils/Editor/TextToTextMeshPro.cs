using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UrUtils
{
    public static class TextToTextMeshPro
    {
        class TextMeshProSettings
        {
            public bool Enabled;
            public FontStyles FontStyle;
            public float FontSize;
            public float FontSizeMin;
            public float FontSizeMax;
            public float LineSpacing;
            public bool EnableRichText;
            public bool EnableAutoSizing;
            public TextAlignmentOptions TextAlignmentOptions;
            public bool WrappingEnabled;
            public TextOverflowModes TextOverflowModes;
            public string Text;
            public Color Color;
            public bool RayCastTarget;
        }

        [MenuItem(UrConstants.MenuRoot + "TMPro Convert/Text To TextMeshPro", false, 4000)]
        static void ConvertToTextMeshPro()
        {
            if (TMP_Settings.defaultFontAsset == null)
            {
                EditorUtility.DisplayDialog("Error", "Assign a default font asset in project settings!", "OK", "");
                return;
            }

            foreach (GameObject gameObject in Selection.gameObjects)
            {
                ConvertTextToTextMeshPro(gameObject);
            }
        }

        [MenuItem(UrConstants.MenuRoot + "TMPro Convert/Text To TextMeshPro (Recursive)", false, 4000)]
        static void ConvertToTextMeshProRecursive()
        {
            if (TMP_Settings.defaultFontAsset == null)
            {
                EditorUtility.DisplayDialog("Error", "Assign a default font asset in project settings!", "OK", "");
                return;
            }

            foreach (GameObject gameObject in Selection.gameObjects)
            {
                ConvertTextToTextMeshProRecursive(gameObject);
            }
        }

        static void ConvertTextToTextMeshProRecursive(GameObject target)
        {
            if (target.GetComponent<Text>() != null)
            {
                ConvertTextToTextMeshPro(target);
            }

            foreach (var child in target.transform)
            {
                if (child is RectTransform childTransform)
                {
                    ConvertTextToTextMeshProRecursive(childTransform.gameObject);
                }
                else
                {
                    Debug.LogWarning($"Child '{child}' of '{target.name}' is not RectTransform, ignoring it");
                }
            }
        }

        static void ConvertTextToTextMeshPro(GameObject target)
        {
            TextMeshProSettings settings = GetTextMeshProSettings(target);
            if (settings == null)
            {
                return;
            }

            Undo.RegisterCompleteObjectUndo(target, $"Replace Text with TextMeshPro on '{target.name}'");
            Undo.DestroyObjectImmediate(target.GetComponent<Text>());

            // Adding TMPro resets transform size for no reason, caching and restoring it manually
            var targetTransform = target.GetComponent<RectTransform>();
            var cachedSize = targetTransform.sizeDelta;

            Undo.AddComponent<TextMeshProUGUI>(target);
            var tmp = target.GetComponent<TextMeshProUGUI>();
            tmp.enabled = settings.Enabled;
            tmp.fontStyle = settings.FontStyle;
            tmp.fontSize = settings.FontSize;
            tmp.fontSizeMin = settings.FontSizeMin;
            tmp.fontSizeMax = settings.FontSizeMax;
            tmp.lineSpacing = settings.LineSpacing;
            tmp.richText = settings.EnableRichText;
            tmp.enableAutoSizing = settings.EnableAutoSizing;
            tmp.alignment = settings.TextAlignmentOptions;
            tmp.enableWordWrapping = settings.WrappingEnabled;
            tmp.overflowMode = settings.TextOverflowModes;
            tmp.text = settings.Text;
            tmp.color = settings.Color;
            tmp.raycastTarget = settings.RayCastTarget;

            Undo.RecordObject(targetTransform, "Restore transform size");
            targetTransform.sizeDelta = cachedSize;

            EditorUtility.SetDirty(target);
        }

        static TextMeshProSettings GetTextMeshProSettings(GameObject gameObject)
        {
            Text uiText = gameObject.GetComponent<Text>();
            if (uiText == null)
            {
                EditorUtility.DisplayDialog("Error", "You must select a Unity UI Text Object to convert.", "OK", "");
                return null;
            }

            return new TextMeshProSettings
            {
                Enabled = uiText.enabled,
                FontStyle = FontStyleToFontStyles(uiText.fontStyle),
                FontSize = uiText.fontSize,
                FontSizeMin = uiText.resizeTextMinSize,
                FontSizeMax = uiText.resizeTextMaxSize,
                LineSpacing = uiText.lineSpacing,
                EnableRichText = uiText.supportRichText,
                EnableAutoSizing = uiText.resizeTextForBestFit,
                TextAlignmentOptions = TextAnchorToTextAlignmentOptions(uiText.alignment),
                WrappingEnabled = HorizontalWrapModeToBool(uiText.horizontalOverflow),
                TextOverflowModes = VerticalWrapModeToTextOverflowModes(uiText.verticalOverflow),
                Text = uiText.text,
                Color = uiText.color,
                RayCastTarget = uiText.raycastTarget
            };
        }

        static bool HorizontalWrapModeToBool(HorizontalWrapMode overflow)
        {
            return overflow == HorizontalWrapMode.Wrap;
        }

        static TextOverflowModes VerticalWrapModeToTextOverflowModes(VerticalWrapMode verticalOverflow)
        {
            return verticalOverflow == VerticalWrapMode.Truncate ? TextOverflowModes.Truncate : TextOverflowModes.Overflow;
        }

        static FontStyles FontStyleToFontStyles(FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case FontStyle.Normal:
                    return FontStyles.Normal;

                case FontStyle.Bold:
                    return FontStyles.Bold;

                case FontStyle.Italic:
                    return FontStyles.Italic;

                case FontStyle.BoldAndItalic:
                    return FontStyles.Bold | FontStyles.Italic;

                default:
                    Debug.LogWarning($"Unhandled font style {fontStyle}");
                    return FontStyles.Normal;
            }
        }

        static TextAlignmentOptions TextAnchorToTextAlignmentOptions(TextAnchor textAnchor)
        {
            switch (textAnchor)
            {
                case TextAnchor.UpperLeft:
                    return TextAlignmentOptions.TopLeft;

                case TextAnchor.UpperCenter:
                    return TextAlignmentOptions.Top;

                case TextAnchor.UpperRight:
                    return TextAlignmentOptions.TopRight;

                case TextAnchor.MiddleLeft:
                    return TextAlignmentOptions.Left;

                case TextAnchor.MiddleCenter:
                    return TextAlignmentOptions.Center;

                case TextAnchor.MiddleRight:
                    return TextAlignmentOptions.Right;

                case TextAnchor.LowerLeft:
                    return TextAlignmentOptions.BottomLeft;

                case TextAnchor.LowerCenter:
                    return TextAlignmentOptions.Bottom;

                case TextAnchor.LowerRight:
                    return TextAlignmentOptions.BottomRight;

                default:
                    Debug.LogWarning($"Unhandled text anchor: {textAnchor}");
                    return TextAlignmentOptions.TopLeft;
            }
        }
    }
}