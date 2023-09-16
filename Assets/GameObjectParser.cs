using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameObjectParser : MonoBehaviour
{
    public Transform parentObject;
    [HideInInspector] public Objects GO;
    private string saveFile;
    private string json;
    void Awake()
    {
        // GO  ;
        // saveFile = Application.dataPath + "/HirarchyData.json";
    }
    void Start()
    {
        GO = new Objects();
        // Parse(parentObject);
        // ConvertToJSON(GO);
    }

    public void Parse(Transform obj)
    {
        for(int i = 0; i < obj.childCount; i++)
        {
            // Debug.Log(obj.GetChild(i).name);
            FillData(obj.GetChild(i));
            
            if(obj.GetChild(i).childCount != 0)
            {
                Parse(obj.GetChild(i));
            }
        }
    }

    private void FillData(Transform obj)
    {
        GameObjectInfo myObj = new GameObjectInfo();
        myObj.name = obj.name;
        myObj.parent = obj.parent.name;
        myObj.data.position = obj.position;
        myObj.data.rotation = obj.rotation.eulerAngles;
        myObj.data.scale = obj.localScale;

        GO.objects.Add(myObj);
    }

    public void ConvertToJSON(Objects go)
    {
        saveFile = Application.dataPath + "/HirarchyData.json";
        json = JsonUtility.ToJson(go);
        File.WriteAllText(saveFile, json);
        Debug.Log(json);
    }

    [ContextMenu("RUN")]
    public void LoadData()
    {   
        string filePath = Application.dataPath + "/HirarchyData.json";
        if(File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            GO = JsonUtility.FromJson<Objects>(jsonText);
            Debug.Log(GO.objects[0].parent);
        }
        else
        {
            Debug.Log("File not fount");
        }
    }
}

[Serializable]
public class Objects
{
    public List<GameObjectInfo> objects = new List<GameObjectInfo>();
}
[Serializable]
public class GameObjectInfo
{
    public string name;
    public string parent;
    public GameObjectData data = new GameObjectData();
}
[Serializable]
public class GameObjectData
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

}
