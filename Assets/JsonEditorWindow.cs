using UnityEngine;
using UnityEditor;

public class JsonEditorWindow : EditorWindow
{
    [MenuItem("Window/JSON Editor")]
    static void OpenWindow()
    {
        JsonEditorWindow window = (JsonEditorWindow)GetWindow(typeof(JsonEditorWindow));
        window.minSize = new Vector2(600, 300);
        window.Show(); 
    }
}
