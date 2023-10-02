using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashLocationManager : MonoBehaviour
{
    public static List<Sprite> _trashPieces = new List<Sprite>();
    public static List<Vector3> _trashLocations = new List<Vector3>(); //in world space
    public static List<GameObject> _trashPrefabs = new List<GameObject>();
    [SerializeField] GameObject[] _trashPrefabObjects;
    private void Start()
    {
        //instaniate all trash pieces and put them at correct spot
        int i = 0;
        foreach (Sprite trash in _trashPieces)
        {
            GameObject trashObj = Instantiate(_trashPrefabs[i]);
            trashObj.GetComponent<SpriteRenderer>().sprite = trash;
            trashObj.transform.position = _trashLocations[i];
            //lock object
            trashObj.GetComponent<ILockable>().OnLock();
            i++;
        }

    }
    public void LogTrashLocations()
    {
        Clear();
        List<ILockable> lockables = new List<ILockable>();
        // Find all lockable objects in the scene
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is ILockable)
                lockables.Add(allScripts[i] as ILockable);
        }

        //if the object is locked then log it
        foreach (ILockable lockable in lockables)
        {
            if (lockable.IsLocked)
            {

                //find the trash peice prefab object with the same sprite
                foreach (GameObject trashPrefabObject in _trashPrefabObjects)
                {
                    if (trashPrefabObject.GetComponent<SpriteRenderer>().sprite == lockable.gameObject.GetComponent<SpriteRenderer>().sprite)
                    {
                        _trashPrefabs.Add(trashPrefabObject);
                    }
                }
                _trashPieces.Add(lockable.gameObject.GetComponent<SpriteRenderer>().sprite);
                _trashLocations.Add(lockable.Position);
            }

        }
    }
    
    public static void Clear()
    {
        _trashPieces.Clear();
        _trashLocations.Clear();
    }

    public static int GetTrashCount()
    {
        return _trashPieces.Count;
    }
}



