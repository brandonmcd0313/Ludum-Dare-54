using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(ClickAndDrag2D))]
[RequireComponent(typeof(Collider2D))]
public class TrashBehaviour : MonoBehaviour, ILockable
{
    bool isInBin;
    bool isLocked;

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    public bool IsLocked
    {
        get
        {
            return isLocked;
        }
    }

    public void OnPickup()
    {
        if (isInBin)
        {
            //destroy the closet gameobject with tag "Stink"
           
            Destroy(FindClosestObjectWithTag("Stink"));

        }
        isInBin = false;
    }

    public void OnLockAttempt()
    {
        //see if coverring any objects
        print("attempted to lock");

        OnLock();

    }

    public void OnLockFailedAttempt()
    {
        isLocked = false;
        //make sprite white
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void OnLock()
    {
        isLocked = true;
        //make sprite red
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public Collider2D Collider
    {
        get
        {
            // Check if the component exists before returning it
            if (TryGetComponent(out Collider2D collider))
            {
                return collider;
            }
            else
            {
                // Component doesn't exist or has been destroyed
                return null;
            }

        }
    }
    

    GameObject FindClosestObjectWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        //find the closest one
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }
        return closestObject;
    }
}

