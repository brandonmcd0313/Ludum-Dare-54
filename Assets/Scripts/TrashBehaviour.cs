using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(ClickAndDrag2D))]
public class TrashBehaviour : MonoBehaviour, ILockable
{
    bool isInBin;
    bool isLocked;
    Vector3 ILockable.Position
    {
        get
        {
            return transform.position;
        }
    }

    bool ILockable.IsLocked
    {
        get
        {
            return isLocked;
        }
    }


    //assign self a random sprite

     public void OnPickup()
    {
        //remove the closest stink effect
    }

    void ILockable.OnLockAttempt()
    {
        //see if coverring any objects
        print("attempted to lock");
    }

    void ILockable.OnLock()
    {
        
    }

    Vector2[] ILockable.GetVertices()
    {
        //return the vertices of this objects collider using the bounds
        Bounds objectBounds = GetComponent<Collider2D>().bounds;
        Vector2[] vertices = new Vector2[4];
        vertices[0] = new Vector2(objectBounds.max.x, objectBounds.max.y);
        vertices[0] = new Vector2(objectBounds.max.x, objectBounds.min.y);
        vertices[0] = new Vector2(objectBounds.min.x, objectBounds.min.y);
        vertices[0] = new Vector2(objectBounds.min.x, objectBounds.max.y);
        return vertices;
    }
}

