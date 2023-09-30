using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class LockZone2D : MonoBehaviour
{
    List<ILockable> _lockables = new List<ILockable>();

    public float edgeRadius = 0.1f; // Set your desired edge radius
    private EdgeCollider2D edgeCollider;
    private PolygonCollider2D fitZone;

    void Start()
    {
        // Create an EdgeCollider2D and set its points to match the polygonRegion
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        edgeCollider.points = GetComponent<PolygonCollider2D>().points;
        edgeCollider.edgeRadius = edgeRadius;
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
        // Check all lockable objects to see if they are in the lock zone
        for (int i = 0; i < _lockables.Count; i++)
        {
            if (_lockables[i].Collider == null)
            {
                _lockables.RemoveAt(i);
                i--;
                continue;
            }

            if (IsFullyContainedInLockZone(_lockables[i].Collider))
            {
                _lockables[i].OnLockAttempt();
            }
            else
            {
                _lockables[i].OnLockFailedAttempt();
            }
        }
    }

    bool IsFullyContainedInLockZone(Collider2D colliderToCheck)
    {
        // Get the bounds of the fitZone
        Bounds fitZoneBounds = fitZone.bounds;

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
