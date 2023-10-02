using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashLocationManager : MonoBehaviour
{
    public static List<GameObject> _trashPieces = new List<GameObject>();
    public static List<Vector3> _trashLocations = new List<Vector3>(); //in world space

    private void Start()
    {
        //instaniate all trash pieces and put them at correct spot
        int i = 0;
        foreach (GameObject trash in _trashPieces)
        {
            GameObject trashObj = Instantiate(trash);
            trashObj.transform.position = _trashLocations[i];
            //lock object
            trashObj.GetComponent<ILockable>().OnLock();
            i++;
        }

    }
    public void LogTrashLocations()
    {
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
                _trashPieces.Add(lockable.gameObject);
                _trashLocations.Add(lockable.Position);
            }

        }
    }
}


