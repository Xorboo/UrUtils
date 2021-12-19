// Script modified and extended by Xorboo 2021. MIT
// Original by:
// Copyright Cape Guy Ltd. 2015. http://capeguy.co.uk.
// Provided under the terms of the MIT license -
// http://opensource.org/licenses/MIT. Cape Guy accepts
// no responsibility for any damages, financial or otherwise,
// incurred as a result of using this code.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace UrUtils
{
    /// <summary>
    /// This script saves the current project and scene (if there is one) whenever the Unity editor enters play mode.
    /// For more information see https://capeguy.co.uk/2015/07/unity-auto-save-on-play-in-editor/.
    /// </summary>
    [InitializeOnLoad]
    public static class SaveAllOnEnterPlayMode
    {
        static SaveAllOnEnterPlayMode()
        {
#if UNITY_2017_2_OR_NEWER
            EditorApplication.playModeStateChanged += stateChange =>
            {
                if (stateChange == PlayModeStateChange.ExitingEditMode)
                    OnLeaveEditMode();
            };
#else
			EditorApplication.playmodeStateChanged = () =>
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
                    OnLeaveEditMode();
            };
#endif
        }

        static void OnLeaveEditMode()
        {
            if (!ShouldTrySaving || ShouldIgnoreCurrentScene)
                return;

            int count = SceneManager.sceneCount;
            Debug.Log($"Auto-Saving {count} scene{(count > 1 ? "s" : "")} and assets before entering play mode: {string.Join(", ", OpenSceneNames)}");
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }


        const string TagFilename = "urutils.saveonplay";
        static string FilePath => Path.Combine(Path.GetDirectoryName(Application.dataPath), TagFilename);
        static bool ShouldTrySaving => File.Exists(FilePath);

        static bool ShouldIgnoreCurrentScene
        {
            get
            {
                // Ignore empty or unity test runner scene
                string currentSceneName = SceneManager.GetActiveScene().name;
                return string.IsNullOrEmpty(currentSceneName) || currentSceneName.StartsWith("InitTestScene");
            }
        }

        static IEnumerable<string> OpenSceneNames =>
            Enumerable.Range(0, SceneManager.sceneCount).Select(i => SceneManager.GetSceneAt(i).name);


        #region Menu Item

        const string SaveMenuName = "UrUtils/Save Scene On Play";

        [MenuItem(SaveMenuName)]
        static void ToggleSave()
        {
            if (ShouldTrySaving)
                File.Delete(FilePath);
            else
                File.WriteAllText(FilePath, $"Automatically save scene when entering Play Mode. See [{SaveMenuName}].\nConsider adding this file to gitignore :)\n\nMade by Xorboo");
        }

        [MenuItem (SaveMenuName, true)]
        public static bool ToggleSaveValidate ()
        {
            Menu.SetChecked(SaveMenuName, ShouldTrySaving);
            return true;
        }

        #endregion
    }
}
