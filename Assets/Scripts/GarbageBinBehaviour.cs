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

    // Start is called before the first frame update
    void Start()
    {
        SpawnTrash();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTrash()
    {
        //pick a number between 0 and 2 to determine the amount of trash to spawn
        int trashAmount = Random.Range(0, 3);

        //spawn that amount of trash and stink
        for (int i = 0; i < trashAmount; i++)
        {
            GameObject newObj = Instantiate(_trashObjects[Random.Range(0, _trashObjects.Length)], transform.position, Quaternion.identity);
            newObj.transform.parent = transform;
        }
        for (int i = 0; i < trashAmount; i++)
        {
            GameObject newObj = Instantiate(_stinkObjects[Random.Range(0, _trashObjects.Length)], transform.position, Quaternion.identity);
            newObj.transform.parent = transform;
        }
    }

    void MoveToPosition()
    {
        
    }
}
