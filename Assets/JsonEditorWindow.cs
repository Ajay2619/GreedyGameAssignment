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
            if(GUILayout.Button("LOAD"))
            {
                gameObjectParser.LoadData(gameObjectParser.filePath);
            }

        GUILayout.Space(10);
        GUILayout.Label("-------------------------------------------------------------------------------------------------");
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        
            if(GUILayout.Button("READ FROM HIERARCHY"))
            {
                gameObjectParser.ReadHierarchy(gameObjectParser.parentObject);
            }

            if(GUILayout.Button("READ FROM INSPECTOR"))
            {
                gameObjectParser.ApplyChanges();
            }
            
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        GUILayout.Label("-------------------------------------------------------------------------------------------------");
        GUILayout.Space(10);

            // gameObjectParser.filePath = EditorGUILayout.TextField("File Path", gameObjectParser.filePath);
            
            if(GUILayout.Button(new GUIContent("SAVE", "amazing tool tip")))
            {
                // gameObjectParser.ApplyChanges();
                // gameObjectParser.ReadHierarchy();
                gameObjectParser.SaveToJSON(gameObjectParser.GO, gameObjectParser.filePath);
            }

        GUILayout.Space(10);
        GUILayout.Label("-------------------------------------------------------------------------------------------------");

        GUILayout.Space(20);

        foreach(var obj in gameObjectParser.GO.objects)
        {
            GUILayout.Space(10);
            EditorGUIUtility.labelWidth = 60;

            GUILayout.Label(obj.name);

            obj.data.position = EditorGUILayout.Vector3Field("Position", obj.data.position);
            
            EditorGUILayout.BeginHorizontal();

                GUILayout.Label("Rotation");

                EditorGUIUtility.labelWidth = 20;

                obj.data.rotation.x = EditorGUILayout.FloatField("X", obj.data.rotation.x);
                obj.data.rotation.y = EditorGUILayout.FloatField("Y", obj.data.rotation.y);
                obj.data.rotation.z = EditorGUILayout.FloatField("Z", obj.data.rotation.z);
                obj.data.rotation.w = EditorGUILayout.FloatField("W", obj.data.rotation.w);

            EditorGUILayout.EndHorizontal();
            
            EditorGUIUtility.labelWidth = 60;
            
            obj.data.scale = EditorGUILayout.Vector3Field("Scale", obj.data.scale);
        }
            
    }
}
