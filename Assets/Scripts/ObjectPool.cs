using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> objects = new List<GameObject>();

    public void Init()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            objects.Add(gameObject.transform.GetChild(i).gameObject);
        }
        SetActiveFalse();
    }
    public void SetActiveFalse()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
    public void AddObject(GameObject obj)
    {
        objects.Add(obj);
    }
    public GameObject GetObject()
    {
        foreach (GameObject obj in objects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        Debug.Log("Can't add object");
        return null;
    }
    public List<GameObject> getObjects()
    {
        return objects;
    }
}

