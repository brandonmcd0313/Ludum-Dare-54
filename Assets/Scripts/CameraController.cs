using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _leftBound;
    [SerializeField] float _rightBound;
    
    private void Start()
    {
    }

    private void Update()
    {

        //use horizontal controls to pan camera right and left
        //if greater than a bound dont move
        if (transform.position.x < _leftBound && Input.GetAxis("Horizontal") < 0)
        {
            return;
        }
        if (transform.position.x > _rightBound && Input.GetAxis("Horizontal") > 0)
        {
            return;
        }
        
        transform.position += new Vector3(Input.GetAxis("Horizontal") * _speed *Time.deltaTime,0, 0); 
        
    }


}