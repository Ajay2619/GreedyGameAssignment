using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameObjectParser))]
public class JsonEditorWindow : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var gameObjectParser = target as GameObjectParser;
        
        GUILayout.Space(20);

        foreach(var obj in gameObjectParser.GO.objects)
        {
            GUILayout.Space(10);
            GUILayout.Label(obj.name);
            obj.data.position = EditorGUILayout.Vector3Field("Position", obj.data.position);
            obj.data.rotation = EditorGUILayout.Vector3Field("Rotation", obj.data.rotation);
            obj.data.scale = EditorGUILayout.Vector3Field("Scale", obj.data.scale);
        }

        GUILayout.BeginHorizontal();

            if(GUILayout.Button("SAVE"))
            {
                //apply changes to game objects
                gameObjectParser.ConvertToJSON(gameObjectParser.GO);
                Debug.Log("SAVED");
            }
            if(GUILayout.Button("LOAD"))
            {
                gameObjectParser.GO = new Objects();
                gameObjectParser.Parse(gameObjectParser.parentObject);
                Debug.Log("LOADED");
            }
            
        GUILayout.EndHorizontal();
    }
}
