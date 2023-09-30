using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LockZone2D : MonoBehaviour
{
    List<ILockable> _lockables = new List<ILockable>();
    void Start()
    {
        //find all lockable objects in the scene
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is ILockable)
                _lockables.Add(allScripts[i] as ILockable);
        }
        ClickAndDrag2D.OnObjectRelease += CheckForObjectsToLock;
    }
   
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckForObjectsToLock()
    {
        //check all lockable objects to see if they are in the lock zone
        for (int i = 0; i < _lockables.Count; i++)
        {
            if (_lockables[i].IsLocked)
                continue;
            if (CheckIfVerticesContainedInLockZone(_lockables[i].GetVertices()))
            {
                _lockables[i].OnLockAttempt();
            }
        }


    }

    bool CheckIfVerticesContainedInLockZone(Vector2[] vertices)
    {
        foreach (Vector2 vertice in vertices)
        {
            if (!GetComponent<Collider2D>().bounds.Contains(vertice))
            {
                return false;
            }
        }
        return true;
    }
}
