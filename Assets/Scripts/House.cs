using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] Vector2 _binPosition;
    [SerializeField] GameObject  _binPrefab;
    public bool HasBeenVisited = false;
    
    private void Start()
    {
        //create a bin
        GameObject bin = Instantiate(_binPrefab);
        bin.transform.parent = transform;
        bin.transform.localPosition = _binPosition;
    }
}
