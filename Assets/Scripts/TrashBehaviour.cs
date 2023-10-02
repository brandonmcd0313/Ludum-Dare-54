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

    void Start()
    {
        isInBin = true;
     
    }
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

    public bool IsInBin
    {
        get
        {
            return isInBin;
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
        print("OnLockAttempt");
        //see if coverring any objects

        //check if any colliders with the tag "Trash" are inside this objects collider
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Trash"))
            {
                
                //if so, no lock
                OnLockFailedAttempt();
                return;
            }
        }
        OnLock();

    }

    public void OnLockFailedAttempt()
    {
        print("failed lock");
        isLocked = false;
        //gravity works on this object now
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        //enable ability to collide
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //stop rotating
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }
    public void OnLock()
    {
        print("lock");
        isLocked = true;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        //disable rigidbodys ability to collide
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
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

