using System.IO;
using UnityEditor;
using UnityEngine;

namespace UrUtils
{
    public static class MonoBehaviourTemplateReplacement
    {
        [MenuItem(UrConstants.MenuRoot + "ScriptTemplate/Print Template")]
        static void PrintTemplate()
        {
            Debug.Log($"Put this text into your '{TemplatePath}' file:");
            Debug.Log(Template);
        }

        [MenuItem(UrConstants.MenuRoot + "ScriptTemplate/Override Template")]
        static void ReplaceTemplate()
        {
            if (!File.Exists(TemplatePath))
            {
                Debug.LogError($"File template not found in '{TemplatePath}'");
                return;
            }

            string currentText = File.ReadAllText(TemplatePath);
            Debug.Log($"Current template:\n{currentText}");

            File.WriteAllText(TemplatePath, Template);
            Debug.Log($"Saved template to '{TemplatePath}'");
        }

        static string TemplatePath => Path.Combine(EditorPath, TemplateInnerPath, TemplateFileName);
        static string EditorPath => Path.GetDirectoryName(EditorApplication.applicationPath);
        static string TemplateInnerPath => @"Data/Resources/ScriptTemplates";
        static string TemplateFileName => "81-C# Script-NewBehaviourScript.cs.txt";
        static string Template =>
            @"using UnityEngine;


#ROOTNAMESPACEBEGIN#
public class #SCRIPTNAME# : MonoBehaviour
{
    #region Unity

    #endregion

    
}
#ROOTNAMESPACEEND#
";
    }
}
