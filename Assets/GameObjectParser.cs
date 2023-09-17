using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameObjectParser : MonoBehaviour
{
    private Transform parentObject;
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

    public void ReadHierarchy()
    {
        GO = new Objects();
        FillData(parentObject);
        Parse(parentObject);
    }
    private void Parse(Transform parent)
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            // Debug.Log(parent.GetChild(i).name);
            FillData(parent.GetChild(i));
            
            if(parent.GetChild(i).childCount != 0)
            {
                Parse(parent.GetChild(i));
            }
        }
    }

    private void FillData(Transform obj)
    {
        GameObjectInfo myObj = new GameObjectInfo();
        myObj.name = obj.name;
        myObj.childCount = obj.childCount;
        myObj.data.position = obj.localPosition;
        myObj.data.rotation = obj.localRotation;
        myObj.data.scale = obj.localScale;

        GO.objects.Add(myObj);
    }

    public void SaveToJSON(Objects go)
    {
        saveFile = Application.dataPath + "/HirarchyData.json";
        json = JsonUtility.ToJson(go);
        File.WriteAllText(saveFile, json);
        Debug.Log(json);
    }
    
    int index = 0;
    [SerializeField] Transform[] transformsHiererchy;
    public void LoadData()
    {   
        GO = new Objects();
        index = 0;
        
        string filePath = Application.dataPath + "/HirarchyData.json";
        if(File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            GO = JsonUtility.FromJson<Objects>(jsonText);
            transformsHiererchy = new Transform[GO.objects.Count];
            Debug.Log(GO.objects.Count);
            Debug.Log(transformsHiererchy.Length);
            transformsHiererchy[index] = parentObject = new GameObject(GO.objects[0].name).transform;
            Debug.Log(index);
            InstantiateObjects(GO.objects[index].childCount, transformsHiererchy[index]);
        }
        else
        {
            Debug.Log("File not fount");
        }
    }

    private void InstantiateObjects(int childCount , Transform parent)
    {
        for(int i = 1; i <= childCount; i++)
        {
            index++;
            transformsHiererchy[index] = new GameObject(GO.objects[index].name).transform;
            transformsHiererchy[index].parent = parent;
            Debug.Log(transformsHiererchy[index].name);
            if(GO.objects[index].childCount != 0)
            {
                InstantiateObjects(GO.objects[index].childCount, transformsHiererchy[index]);
            }
        }
    }

    public void ApplyChanges()
    {
        int index = 0;
        foreach (var obj in GO.objects)
        {
            transformsHiererchy[index].localPosition = obj.data.position;
            transformsHiererchy[index].localRotation = obj.data.rotation;
            transformsHiererchy[index].localScale = obj.data.scale;
            index++;
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
    public int childCount;
    public GameObjectData data = new GameObjectData();
}
[Serializable]
public class GameObjectData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

}
