using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameObjectParser))]
public class JsonEditorWindow : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var gameObjectParser = target as GameObjectParser;
        Vector4 q;
        
        GUILayout.Space(20);

        foreach(var obj in gameObjectParser.GO.objects)
        {
            GUILayout.Space(10);
            GUILayout.Label(obj.name);
            q = new Vector4(obj.data.rotation.x, obj.data.rotation.y, obj.data.rotation.z, obj.data.rotation.w);

            obj.data.position = EditorGUILayout.Vector3Field("Position", obj.data.position);
            q = EditorGUILayout.Vector4Field("Rotation", q);
            obj.data.rotation = new Quaternion(q.w, q.x, q.y, q.z);
            obj.data.scale = EditorGUILayout.Vector3Field("Scale", obj.data.scale);
        }

        GUILayout.BeginHorizontal();

            if(GUILayout.Button("SAVE"))
            {
                gameObjectParser.ApplyChanges();
                gameObjectParser.ReadHierarchy();
                gameObjectParser.SaveToJSON(gameObjectParser.GO);
            }
            if(GUILayout.Button("LOAD"))
            {
                gameObjectParser.LoadData();
            }
            
        GUILayout.EndHorizontal();
    }
}
