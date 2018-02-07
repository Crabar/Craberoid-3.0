using System.Timers;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class Autosave
    {
        static Autosave()
        {
            var nextTime = EditorApplication.timeSinceStartup + 60;

            EditorApplication.update += () =>
            {
                if (!EditorApplication.isPlaying && nextTime < EditorApplication.timeSinceStartup)
                {
                    Debug.Log("Auto-saving all open scenes...");
                    EditorSceneManager.SaveOpenScenes();
                    AssetDatabase.SaveAssets();
                    nextTime = EditorApplication.timeSinceStartup + 60;
                }
            };
        }
    }
}
