#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace UrUtils.GitIntegration
{
    [InitializeOnLoad]
    public class SmartMergeRegistrator
    {
        private const string _smartMergeRegistratorEditorPrefsKey = "smart_merge_installed";
        private const int _version = 1;
        private static readonly string _versionKey = $"{_version}_{Application.unityVersion}";

        [MenuItem("Tools/Git/SmartMerge Registration")]
        private static void SmartMergeRegister()
        {
            try
            {
                var unityYamlMergePath = EditorApplication.applicationContentsPath + "/Tools" + "/UnityYAMLMerge.exe";
                GitUtils.ExecuteGitWithParams("config merge.unityyamlmerge.name \"Unity SmartMerge (UnityYamlMerge)\"");
                GitUtils.ExecuteGitWithParams($"config merge.unityyamlmerge.driver \"\\\"{unityYamlMergePath}\\\" merge -h -p --force --fallback none %O %B %A %A\"");
                GitUtils.ExecuteGitWithParams("config merge.unityyamlmerge.recursive binary");
                EditorPrefs.SetString(_smartMergeRegistratorEditorPrefsKey, _versionKey);
                Debug.Log($"Succesfuly registered UnityYAMLMerge with path {unityYamlMergePath}");
            }
            catch (Exception e)
            {
                Debug.Log($"Fail to register UnityYAMLMerge with error: {e}");
            }
        }

        [MenuItem("Tools/Git/SmartMerge Unregistration")]
        private static void SmartMergeUnRegister()
        {
            GitUtils.ExecuteGitWithParams("config --remove-section merge.unityyamlmerge");
            Debug.Log($"Succesfuly unregistered UnityYAMLMerge");
        }

        //Unity calls the static constructor when the engine opens
        static SmartMergeRegistrator()
        {
            var instaledVersionKey = EditorPrefs.GetString(_smartMergeRegistratorEditorPrefsKey);
            if (instaledVersionKey != _versionKey)
            {
                SmartMergeRegister();
            }
        }
    }
}
#endif