using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickAndDrag2D : MonoBehaviour
{
    public static Action OnObjectRelease;
    
    public bool IsMovable = true;
    [SerializeField] AudioClip _pickupSound;
    [SerializeField] AudioClip _dropSound;
    //offset from object to mouse position, determined on pickup
    private Vector3 _offset = new Vector3();
    bool _isFollowingMouse = false;
    float _rotationSpeed = 60;
    // Use this for initialization
    void Start()
    {
        //set it so that the mouse can't go off-screen
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        //if mouse is released stop following the mouse
        if (!Input.GetKey(KeyCode.Mouse0) && _isFollowingMouse)
        {
            OnMouseUp();
        }

        Vector3 mousePosition = Input.mousePosition;

        if (_isFollowingMouse)
        {
            //translate mouse position to world space
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = transform.position.z; //keep this object at the same z value
            transform.position = worldPosition + _offset;

            //if user is pressing R rotate this object
            if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(0f, 0f, Time.deltaTime * _rotationSpeed);
            }
        }

        //if mouse goes off-screen, drop piece and snap to on-screen position
        Vector3 mousePositionNormalized = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if ((mousePositionNormalized.x > 1 || mousePositionNormalized.y > 1) && _isFollowingMouse)
        {
            //snap to larger value (floor)
            _isFollowingMouse = false;
            transform.position = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));
        }
        if ((mousePositionNormalized.x < 0 || mousePositionNormalized.y < 0) && _isFollowingMouse)
        {
            //snap to smaller value (ceil)
            _isFollowingMouse = false;
            transform.position = new Vector3(Mathf.Ceil(transform.position.x), Mathf.Ceil(transform.position.y));
        }
    }

    void OnMouseDown()
    {
                print(gameObject.name + " was clicked");
        if (IsMovable)
        {
            //set offset, follow mouse
            _offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _offset.z = 0;
            _isFollowingMouse = true;
            if (_pickupSound && GetComponent<AudioSource>())
            { GetComponent<AudioSource>().PlayOneShot(_pickupSound); }

            if (gameObject.GetComponent<TrashBehaviour>() != null)
            {
                gameObject.GetComponent<TrashBehaviour>().OnPickup();
            }
        }
    }

    private void OnMouseUp()
    {
        if(!_isFollowingMouse)
        {
            return;
        }
        //stop following mouse
        _isFollowingMouse = false;
        if (_dropSound & GetComponent<AudioSource>())
        { GetComponent<AudioSource>().PlayOneShot(_dropSound); }

        //if offject is off-screen, snap to on-screen position
        Vector3 objecctPositionNormalized = Camera.main.ScreenToViewportPoint(transform.position);
        if ((objecctPositionNormalized.x > 1 || objecctPositionNormalized.y > 1) && _isFollowingMouse)
        {
            //snap to larger value (floor)
            _isFollowingMouse = false;
            transform.position = new Vector3(Mathf.Floor(transform.position.x), Mathf.Floor(transform.position.y));
        }
        if ((objecctPositionNormalized.x < 0 || objecctPositionNormalized.y < 0) && _isFollowingMouse)
        {
            //snap to smaller value (ceil)
            _isFollowingMouse = false;
            transform.position = new Vector3(Mathf.Ceil(transform.position.x), Mathf.Ceil(transform.position.y));
        }

        OnObjectRelease?.Invoke();
    }
}
