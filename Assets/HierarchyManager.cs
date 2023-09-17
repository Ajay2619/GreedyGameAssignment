using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HierarchyManager : MonoBehaviour
{
    public Transform parentObject;
    [HideInInspector] public ObjectTemplets templets;
    private string saveFile;
    private string json;
    public string filePath = "/HierarchyData";

    void Start()
    {
        templets = new ObjectTemplets();
    }

    public void ReadHierarchy(Transform parent)
    {
        templets = new ObjectTemplets();
        FillData(parent);
        Parse(parent);
    }
    private void Parse(Transform parent)
    {
        for(int i = 0; i < parent.childCount; i++)
        {
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

        templets.objects.Add(myObj);
    }

    public void SaveToJSON(ObjectTemplets templets, string path)
    {
        saveFile = Application.dataPath + path;
        json = JsonUtility.ToJson(templets);
        File.WriteAllText(saveFile, json);
        Debug.Log(json);
    }
    
    int index = 0;
    Transform[] transformsHiererchy;
    public void LoadData(string path)
    {   
        templets = new ObjectTemplets();
        index = 0;
        
        string filePath = Application.dataPath + path;
        if(File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            templets = JsonUtility.FromJson<ObjectTemplets>(jsonText);
            transformsHiererchy = new Transform[templets.objects.Count];
            Debug.Log(templets.objects.Count);
            Debug.Log(transformsHiererchy.Length);
            transformsHiererchy[index] = parentObject = new GameObject(templets.objects[0].name).transform;
            SetData(index);
            Debug.Log(index);
            InstantiateObjects(templets.objects[index].childCount, transformsHiererchy[index]);
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
            transformsHiererchy[index] = new GameObject(templets.objects[index].name).transform;
            transformsHiererchy[index].parent = parent;
            SetData(index);
            
            Debug.Log(transformsHiererchy[index].name);
            if(templets.objects[index].childCount != 0)
            {
                InstantiateObjects(templets.objects[index].childCount, transformsHiererchy[index]);
            }
        }
    }

    private void SetData(int index)
    {
        transformsHiererchy[index].localPosition = templets.objects[index].data.position;
        transformsHiererchy[index].localRotation = templets.objects[index].data.rotation;
        transformsHiererchy[index].localScale = templets.objects[index].data.scale;
    }

    public void ApplyChanges()
    {
        int index = 0;
        foreach (var obj in templets.objects)
        {
            transformsHiererchy[index].localPosition = obj.data.position;
            transformsHiererchy[index].localRotation = obj.data.rotation;
            transformsHiererchy[index].localScale = obj.data.scale;
            index++;
        }
    }
}


[Serializable]
public class ObjectTemplets
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
