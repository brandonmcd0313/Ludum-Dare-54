using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingCameraController : MonoBehaviour
{
    [SerializeField] GameObject _truckObject;
    [SerializeField] float _cameraYOffset;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            this.transform.position = new Vector3(_truckObject.transform.position.x, _truckObject.transform.position.y + _cameraYOffset, this.transform.position.z);
       
    }
}
