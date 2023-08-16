using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DestroyObjectTracker : MonoBehaviour
{
    private static DestroyObjectTracker instanceDestroy;
    private HashSet<string> destroyedObjectsNames = new HashSet<string>();

    private void Awake()
    {
        if(instanceDestroy == null)
        {
            instanceDestroy = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void MarkObjectAsDestroyed(GameObject obj)
    {
        if(instanceDestroy != null && obj != null)
        {
            instanceDestroy.destroyedObjectsNames.Add(obj.name);
        }
    }

    public static bool IsDestroyed(GameObject obj)
    {
        return instanceDestroy != null && instanceDestroy.destroyedObjectsNames.Contains(obj.name);
    }
    //Kann verwendet werden damit das Object wieder in die Liste aufgenommen wird
    public static void RemoveDestroyedObject(GameObject obj)
    {
        if (instanceDestroy != null && obj != null)
        {
            instanceDestroy.destroyedObjectsNames.Remove(obj.name);
        }
    }

    //Die ganze Liste kann wieder zurück gesetzt werden 
    public static void ClearDestroyedObjects()
    {
        if (instanceDestroy != null)
        {
            instanceDestroy.destroyedObjectsNames.Clear();
        }
    }

    public static void PrintDestroyedObjectList()
    {
        if(instanceDestroy != null)
        {
            Debug.Log("Destroyed Object List:");
            foreach (string objName in instanceDestroy.destroyedObjectsNames)
            {
                Debug.Log(objName);
            }
        }
    }
}
