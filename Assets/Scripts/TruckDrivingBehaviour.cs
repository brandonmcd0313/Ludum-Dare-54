using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TruckDrivingBehaviour : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotationSpeed;
    public bool IsMovingUpwards;
    Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetClosestBin().GetComponent<GarbageBinClickable>().SetToGlow();
        //back and forth movement
        _rb.AddForce(transform.up * _speed * -Input.GetAxis("Vertical") * Time.fixedDeltaTime);
        //rotation
        _rb.rotation += (-Input.GetAxis("Horizontal") * _rotationSpeed * Time.fixedDeltaTime);
        //clamp the car speed
        //_rb.velocity = Vector2.ClampMagnitude(_rb.velocity, _speed);

        if (Input.GetKey(KeyCode.Space))
        {
            //start aproaching zero for velocity
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, 2f * Time.fixedDeltaTime);
        }
        //if the car is moving upwards, set the bool to true
        if (_rb.velocity.y > 0)
        {
            IsMovingUpwards = true;
        }
        else
        {
            IsMovingUpwards = false;
        }
        
    }

    public GameObject GetClosestBin()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Bin");
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
