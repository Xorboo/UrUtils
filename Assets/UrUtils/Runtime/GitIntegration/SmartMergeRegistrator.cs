#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace UrUtils.GitIntegration
{
    [InitializeOnLoad]
    public class SmartMergeRegistrator
    {
        const string SmartMergeRegistratorEditorPrefsKey = "smart_merge_installed";
        const int Version = 1;
        static readonly string VersionKey = $"{Version}_{Application.unityVersion}";

        [MenuItem("Tools/Git/SmartMerge Registration")]
        static void SmartMergeRegister()
        {
            try
            {
                var unityYamlMergePath = EditorApplication.applicationContentsPath + "/Tools" + "/UnityYAMLMerge.exe";
                GitUtils.ExecuteGitWithParams("config merge.unityyamlmerge.name \"Unity SmartMerge (UnityYamlMerge)\"");
                GitUtils.ExecuteGitWithParams($"config merge.unityyamlmerge.driver \"\\\"{unityYamlMergePath}\\\" merge -h -p --force --fallback none %O %B %A %A\"");
                GitUtils.ExecuteGitWithParams("config merge.unityyamlmerge.recursive binary");
                EditorPrefs.SetString(SmartMergeRegistratorEditorPrefsKey, VersionKey);
                Debug.Log($"Successfully registered UnityYAMLMerge with path {unityYamlMergePath}");
            }
            catch (Exception e)
            {
                Debug.Log($"Fail to register UnityYAMLMerge with error: {e}");
            }
        }

        [MenuItem("Tools/Git/SmartMerge Unregistration")]
        static void SmartMergeUnRegister()
        {
            GitUtils.ExecuteGitWithParams("config --remove-section merge.unityyamlmerge");
            Debug.Log($"Succesfully unregistered UnityYAMLMerge");
        }

        //Unity calls the static constructor when the engine opens
        static SmartMergeRegistrator()
        {
            var installedVersionKey = EditorPrefs.GetString(SmartMergeRegistratorEditorPrefsKey);
            if (installedVersionKey != VersionKey)
            {
                SmartMergeRegister();
            }
        }
    }
}
#endif