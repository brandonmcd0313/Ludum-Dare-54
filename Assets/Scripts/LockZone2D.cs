using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class LockZone2D : MonoBehaviour
{
    List<ILockable> _lockables = new List<ILockable>();

    public float edgeRadius = 0.025f; // Set your desired edge radius
    private EdgeCollider2D edgeCollider;
    private PolygonCollider2D fitZone;

    void Start()
    {
        // Create an EdgeCollider2D and set its points to match the polygonRegion
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        edgeCollider.points = GetComponent<PolygonCollider2D>().points;
        edgeCollider.edgeRadius = edgeRadius;
        //set edge radius to offset pos x = -1 becuase broken
        edgeCollider.offset = new Vector2(-1, 0);
        //make edge colldier a trigger
        edgeCollider.isTrigger = true;
        fitZone = GetComponent<PolygonCollider2D>();

        // Find all lockable objects in the scene
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is ILockable)
                _lockables.Add(allScripts[i] as ILockable);
        }

        // Subscribe to the release event
        ClickAndDrag2D.OnObjectRelease += CheckForObjectsToLock;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckForObjectsToLock()
    {
        _lockables.Clear();
        // Find all lockable objects in the scene
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if (allScripts[i] is ILockable)
                _lockables.Add(allScripts[i] as ILockable);
        }
        
        for (int i = _lockables.Count - 1; i >= 0; i--)
        {
            ILockable lockable = _lockables[i];
            GameObject trash = lockable.gameObject;
            // Check if the lockable object is null or its collider is null (indicating it has been destroyed)
            if (trash == null || trash.GetComponent<Collider2D>() == null)
            {
                _lockables.RemoveAt(i);
                continue;
            }

            if (IsFullyContainedInLockZone(trash.GetComponent<Collider2D>()))
            {
                lockable.OnLockAttempt();
            }
            else
            {
                if (!lockable.IsInBin)
                {
                    lockable.OnLockFailedAttempt();
                }
            }
        }
    }


    bool IsFullyContainedInLockZone(Collider2D colliderToCheck)
    {
        // Get the bounds of the fitZone
        fitZone = GameObject.FindGameObjectWithTag("Truck").GetComponent<PolygonCollider2D>();
        Bounds fitZoneBounds = fitZone.bounds;
        // Create an EdgeCollider2D and set its points to match the polygonRegion
        edgeCollider = GameObject.FindGameObjectWithTag("Truck").AddComponent<EdgeCollider2D>();
        edgeCollider.points = GameObject.FindGameObjectWithTag("Truck").GetComponent<PolygonCollider2D>().points;
        edgeCollider.edgeRadius = edgeRadius;
        //set edge radius to offset pos x = -1 becuase broken
        edgeCollider.offset = new Vector2(-1, 0);
        // Get the local corners of the colliderToCheck
        Vector2[] colliderCorners = new Vector2[4];
        colliderCorners[0] = colliderToCheck.bounds.min;
        colliderCorners[1] = new Vector2(colliderToCheck.bounds.min.x, colliderToCheck.bounds.max.y);
        colliderCorners[2] = colliderToCheck.bounds.max;
        colliderCorners[3] = new Vector2(colliderToCheck.bounds.max.x, colliderToCheck.bounds.min.y);

        /*
        // Transform to world space
        for (int i = 0; i < 4; i++)
        {
            colliderCorners[i] = colliderToCheck.transform.TransformPoint(colliderCorners[i]);
        }
        */
        // Check if all the corners of the collider are inside the fitZone
        for (int i = 0; i < 4; i++)
        {
            if (!fitZoneBounds.Contains(colliderCorners[i]))
            {
                print("not fully contained");
                return false;
            }
        }

        // Check if it's not touching the edge
        bool isTouchingEdge = Physics2D.IsTouching(colliderToCheck, edgeCollider);
        
        if (isTouchingEdge)
        {
            print("touching edge");
            return false;
        }

        return true;
    }

}
