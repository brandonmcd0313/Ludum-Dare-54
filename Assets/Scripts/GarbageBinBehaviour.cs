using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBinBehaviour : MonoBehaviour
{
    //the left and right positions and roations for the bin
    [SerializeField] Vector3 _rightPosition;
    [SerializeField] Vector3 _leftPosition;
    [SerializeField] Quaternion _rightRotation;
    [SerializeField] Quaternion _leftRotation;
    [Space(5)]
    //the prefabs for the stink objects
    [SerializeField] GameObject[] _stinkObjects;
    //the prebads for the trash objects
    [SerializeField] GameObject[] _trashObjects;

    public float edgeRadius = 0.05f; // Set your desired edge radius
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        // Create an EdgeCollider2D and set its points to match the polygonRegion
        edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        edgeCollider.points = GetComponent<PolygonCollider2D>().points;
        edgeCollider.edgeRadius = edgeRadius;
        SpawnTrash();
        
    }
    
    void SpawnTrash()
    {
        //pick a number between 1 and 2 to determine the amount of trash to spawn
        int trashAmount = Random.Range(1, 3);

        //spawn that amount of trash and stink
        for (int i = 0; i < trashAmount; i++)
        {
            GameObject newObj = Instantiate(_trashObjects[Random.Range(0, _trashObjects.Length)], transform.position, Quaternion.identity);
            MoveToTrashBin(newObj);
        }
        for (int i = 0; i < trashAmount; i++)
        {
            Instantiate(_stinkObjects[Random.Range(0, _trashObjects.Length)], transform.position, Quaternion.identity);
        }
    }

    void MoveToTrashBin(GameObject trash)
    {
        while (!IsFullyContainedInZone(trash.GetComponent<Collider2D>()))
        {
            //put the trash at some random place and z rotation, it needs to be fully contained in the bounds of this objects collider
            trash.transform.position = new Vector3(
                Random.Range(transform.position.x - GetComponent<Collider2D>().bounds.extents.x, transform.position.x + GetComponent<Collider2D>().bounds.extents.x),
                Random.Range(transform.position.y - GetComponent<Collider2D>().bounds.extents.y, transform.position.y + GetComponent<Collider2D>().bounds.extents.y),
                transform.position.z);

            //rotate the trash to some random rotation
            trash.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
        
    }

    void MoveToPosition()
    {
        
    }

    bool IsFullyContainedInZone(Collider2D colliderToCheck)
    {
        // Get the bounds of the fitZone
        Bounds fitZoneBounds = this.GetComponent<Collider2D>().bounds;
        

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
