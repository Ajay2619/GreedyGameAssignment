using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(HierarchyManager))]
public class JsonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var gameObjectParser = target as HierarchyManager;
        
        GUILayout.Space(20);
            if(GUILayout.Button(new GUIContent("LOAD", "make sure the file path is correct to LOAD and GENERATE hierarchy from the file")))
            {
                gameObjectParser.LoadData(gameObjectParser.filePath);
            }

        GUILayout.Space(10);
        GUILayout.Label("-------------------------------------------------------------------------------------------------");
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        
            if(GUILayout.Button(new GUIContent("READ FROM HIERARCHY", "Make sure to link the parent object of the hierarchy to \"Parent Object\" field of which you want to generate JSON \n\nYou can read the changes made in the \n SCENE and apply them to the objects \n\n Press save once the changes are confirmed to create JSON file ")))
            {
                gameObjectParser.ReadHierarchy(gameObjectParser.parentObject);
            }

            if(GUILayout.Button(new GUIContent("READ FROM INSPECTOR", "You can read the changes made in the \n INSPECTOR and apply them to the objects \n\nPress save once the changes are confirmed to create JSON file " )))
            {
                gameObjectParser.ApplyChanges();
            }
            
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        GUILayout.Label("-------------------------------------------------------------------------------------------------");
        GUILayout.Space(10);

            
            if(GUILayout.Button(new GUIContent("SAVE", "Save to generate JSON file once you read the changes with the aboce buttons")))
            {
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
