using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarbageBinClickable : MonoBehaviour
{
    [SerializeField] GameObject _truck;
    [SerializeField] Sprite _visited;
    Sprite _glow;
    // Start is called before the first frame update
    void Start()
    {
        _glow = GetComponent<SpriteRenderer>().sprite;
        //set to visited srite
        GetComponent<SpriteRenderer>().sprite = _visited;
        _truck = GameObject.FindGameObjectWithTag("Truck");
        if (transform.parent.GetComponent<House>().HasBeenVisited)
        {
            GetComponent<SpriteRenderer>().sprite = _visited;
        }
        InvokeRepeating("ResetCollider", 0f, 0.1f);
    }

    private void OnMouseDown()
    {
        if (transform.parent.GetComponent<House>().HasBeenVisited)
        {
            GetComponent<SpriteRenderer>().sprite = _visited;
            return;
        }

        //if this is not the closest bin
        if (gameObject != _truck.GetComponent<TruckDrivingBehaviour>().GetClosestBin())
        {
            return;
        }

        //a^2 + b^2 = c^2
        float absoluteVelocitySquared = Mathf.Pow(_truck.GetComponent<Rigidbody2D>().velocity.x, 2) + Mathf.Pow(_truck.GetComponent<Rigidbody2D>().velocity.y, 2);
        float absoluteVelocity = Mathf.Pow(absoluteVelocitySquared, 0.5f);
        //check if the truck is going slow
        if (absoluteVelocity < 5f)
        {
            //set the direction manager direction
            //find if this bin is left or right of the road
            if (transform.position.x > FindClosestObjectWithTag("Road").transform.position.x)
            {
                DirectionManager.isToTheRight = true;
            }
            else
            {
                DirectionManager.isToTheRight = false;
            }
            //tell the parent house it has been visited
            transform.parent.GetComponent<House>().HasBeenVisited = true;
            HouseInfoStorage.SetAsVisitedHouseAt(transform.parent.transform.position);
            //set player pos in house info
            HouseInfoStorage.PlayerPosition = transform.parent.transform.position;
            //load the bin to truck scene
            SceneManager.LoadScene("BinToTruck");
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

    void ResetCollider()
    {
        //set to visited srite
        GetComponent<SpriteRenderer>().sprite = _visited;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    
    
    public void SetToGlow()
    {
        if(!transform.parent.GetComponent<House>().HasBeenVisited)
        {

            GetComponent<SpriteRenderer>().sprite = _glow;
        }
    
    }
}
