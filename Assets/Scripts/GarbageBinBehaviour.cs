using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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



    void Start()
    {
      
        SpawnTrash();
        MoveToPosition();
        UnchildObjects();

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
            newObj.transform.parent = transform;
            //maybe make it a little gray
            float grayValue = Random.Range(0.75f, 1f);
            newObj.GetComponent<SpriteRenderer>().color = new Color(grayValue, grayValue, grayValue);
        }
        for (int i = 0; i < trashAmount; i++)
        {
            GameObject newObj = Instantiate(_stinkObjects[Random.Range(0, _stinkObjects.Length)], transform.position, Quaternion.identity);
            newObj.transform.parent = transform;
        }
    }

    void MoveToTrashBin(GameObject trash)
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        int i = 0; //use to prevent crashing :)

        trash.transform.position = new Vector3(
                Random.Range(bounds.min.x + 0.5f, bounds.max.x - 0.5f),
                Random.Range(bounds.min.y + 0.5f, bounds.max.y - 0.5f),
                transform.position.z);

        //rotate the trash to some random rotation
        trash.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
      
    }

    void MoveToPosition()
    {
        if(DirectionManager.isToTheRight)
        {
            transform.position = _rightPosition;
            transform.rotation = _rightRotation;
        }
        else
        {
            transform.position = _leftPosition;
            transform.rotation = _leftRotation;
        }
    }

    void UnchildObjects()
    {
        //make all children of this object no longer children
        //loop through all children
        int children = transform.childCount;
        for(int i = 0; i < children; i++)
        {

            GameObject child = transform.GetChild(0).gameObject;
            child.transform.parent = null;
            //reload object
            child.SetActive(false);
            child.SetActive(true);
        }
        //remove the collider from this object
        Destroy(GetComponent<Collider2D>());
    }
}

