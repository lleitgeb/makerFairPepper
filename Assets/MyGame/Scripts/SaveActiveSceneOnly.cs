using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class SaveActiveSceneOnly
{
    [MenuItem("File/Save Active Scene Only %&s")] // Shortcut: Alt+Shift+S
    public static void SaveOnlyActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.isDirty)
        {
            EditorSceneManager.SaveScene(activeScene);
            Debug.Log($"Nur die aktive Szene '{activeScene.name}' wurde gespeichert.");
        }
        else
        {
            Debug.Log($"Die aktive Szene '{activeScene.name}' benötigt kein Speichern.");
        }
    }
}
