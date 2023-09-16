using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameObjectParser : MonoBehaviour
{
    [SerializeField] private Transform parentObject;
    private Objects GO = new Objects();
    private string saveFile;
    private string json;
    void Awake()
    {
        saveFile = Application.dataPath + "/hirarchyData.json";
    }
    void Start()
    {
        Parser(parentObject);
        ConvertToObject(parentObject);
        File.WriteAllText(saveFile, json);
    }

    private void Parser(Transform obj)
    {
        for(int i = 0; i < obj.childCount; i++)
        {
            Debug.Log(obj.GetChild(i).name);
            FillData(obj.GetChild(i));
            
            if(obj.GetChild(i).childCount != 0)
            {
                Parser(obj.GetChild(i));
            }
        }
    }

    private void FillData(Transform obj)
    {
        GameObjectInfo myObj = new GameObjectInfo();
        myObj.name = obj.name;
        myObj.parent = obj.parent.name;
        myObj.data.position = obj.position;
        myObj.data.rotation = obj.rotation;
        myObj.data.scale = obj.localScale;

        GO.objects.Add(myObj);
    }

    private void ConvertToObject(Transform obj)
    {
        json = JsonUtility.ToJson(GO);
        Debug.Log(json);
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
    public Quaternion rotation;
    public Vector3 scale;

}
