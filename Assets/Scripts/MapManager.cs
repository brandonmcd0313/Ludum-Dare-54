using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MapManager : MonoBehaviour
{
    //this will infinetly generate new road and houses (with bins)
    [SerializeField] GameObject _playerTruck;
    TruckDrivingBehaviour _truckDrivingBehaviour;
   

    [Tooltip("The amount of units the player has to move up before the next road is generated")]
    [SerializeField] float _regeneratePerUnits;
    [SerializeField] int _numberOfHouses;

    float _refrenceYValue;

    [Space(5)]

    [SerializeField] GameObject _roadPrefab;
    [SerializeField] GameObject[] _housePrefabs;
    [SerializeField] Color[] _acceptableColors;
    

    [SerializeField] GameObject _binPrefab;
    float roadX;

    // Start is called before the first frame update
    void Start()
    {
        GameObject closestRoad = FindClosestObjectWithTag("Road");
        roadX = closestRoad.transform.position.x;
        GameObject newRoad = Instantiate(_roadPrefab,
          new Vector3(roadX, _playerTruck.transform.position.y, 0), Quaternion.identity);
        newRoad.SetActive(false);
        newRoad.SetActive(true);

        _truckDrivingBehaviour = _playerTruck.GetComponent<TruckDrivingBehaviour>();
        _refrenceYValue = 0;

        //make a road where the player starts
       
        Instantiate(_roadPrefab,
            new Vector3(roadX, _playerTruck.transform.position.y, 0), Quaternion.identity);

        if (HouseInfoStorage.HousesA.Count == 0)
        {
            CreateFirstHouses();
        }
    }

    public void ResetAllHouses()
    {
        //destroy all objects with tag house
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        foreach (GameObject house in houses)
        {
            Destroy(house);
        }

        CreateFirstHouses();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player has moved up a certain amount
        if (System.Math.Abs(_playerTruck.transform.position.y - _refrenceYValue) >= _regeneratePerUnits)
        {
            _refrenceYValue = transform.position.y;
            CreateNewRoad();
        }
    }


    void CreateNewRoad()
    {
        Vector3 newSpawnPosition = Vector3.zero;
        //find the closest road 
        GameObject closestRoad = FindClosestObjectWithTag("Road");

        //if player is moving up create one above, if player is moving down create one below

        if (_truckDrivingBehaviour.IsMovingUpwards)
        {
            //find the top of the last road spawned
            float lastRoadTop = closestRoad.transform.position.y + (closestRoad.GetComponent<Collider2D>().bounds.size.y / 2);
            
            newSpawnPosition = new Vector3(roadX,
                lastRoadTop + (_roadPrefab.GetComponent<Collider2D>().bounds.size.y / 2), closestRoad.transform.position.z);

        }
        else
        {
            //find the bottom of the last road spawned
            float lastRoadBottom = closestRoad.transform.position.y - (closestRoad.GetComponent<Collider2D>().bounds.size.y / 2);

            newSpawnPosition = new Vector3(roadX,
                lastRoadBottom - (_roadPrefab.GetComponent<Collider2D>().bounds.size.y / 2), closestRoad.transform.position.z);

        }
        //check if there is already a road at the new spawn position
        GameObject[] currentRoads = GameObject.FindGameObjectsWithTag("Road");
        foreach (GameObject road in currentRoads)
        {
            //set to roadX
            if (road.transform.position.x != roadX)
            {
                road.transform.position = new Vector3(roadX, road.transform.position.y, road.transform.position.z);
            }
            if (road.transform.position.y == newSpawnPosition.y)
            {
                //DO NOT SPAWN NEW ROAD OR HOUSES
                return;
            }
        }
       GameObject newRoad = Instantiate(_roadPrefab);
        newRoad.transform.position = newSpawnPosition;
      //  CreateNewHouseRow();
    }

    void CreateFirstHouses()
    {
        GameObject closestRoad = FindClosestObjectWithTag("Road");
        float leftOfRoadx = closestRoad.transform.position.x - (closestRoad.GetComponent<Collider2D>().bounds.size.x / 2);
       
        float rightOfRoadx = closestRoad.transform.position.x + (closestRoad.GetComponent<Collider2D>().bounds.size.x / 2);
        
        //create house on left and right of road
        GameObject leftHousePrefab = _housePrefabs[Random.Range(0, _housePrefabs.Length)];
        GameObject rightHousePrefab = _housePrefabs[Random.Range(0, _housePrefabs.Length)];

        GameObject leftHouse = Instantiate(leftHousePrefab);
        leftHouse.transform.position = new Vector3(leftOfRoadx - (leftHouse.GetComponent<Collider2D>().bounds.size.x / 2), _playerTruck.transform.position.y, 0);
        
        
        //recolor the two children of the object
        Color one = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
        Color two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
        while (one == two)
        {
            two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
        }
        leftHouse.transform.GetChild(0).GetComponent<SpriteRenderer>().color =one;
        leftHouse.transform.GetChild(1).GetComponent<SpriteRenderer>().color = two;
        HouseInfoStorage.AddHouse(leftHouse, leftHousePrefab);
        
        GameObject rightHouse = Instantiate(rightHousePrefab);
        rightHouse.transform.position = new Vector3(rightOfRoadx + (rightHouse.GetComponent<Collider2D>().bounds.size.x / 2), _playerTruck.transform.position.y, 0);
        print(rightHouse.GetComponent<Collider2D>().bounds.size.x / 2);
        //recolor the two children of the object
        one = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
        two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
        while (one == two)
        {
            two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
        }
        rightHouse.transform.GetChild(0).GetComponent<SpriteRenderer>().color = one;
        rightHouse.transform.GetChild(1).GetComponent<SpriteRenderer>().color = two;

        //flip the object on x axis
        rightHouse.transform.localScale = new Vector3(-1, 1, 1);
        HouseInfoStorage.AddHouse(rightHouse, rightHousePrefab);
        SpawnHousesAbove(rightHouse);
        SpawnHousesAbove(leftHouse);
        SpawnHousesBelow(leftHouse);
        SpawnHousesBelow(rightHouse);

    }
   
    void SpawnHousesBelow(GameObject referenceHouse)
    {
        Vector3 referencePosition = referenceHouse.transform.position;
        for (int i = 0; i < _numberOfHouses; i++)
        {

            GameObject newHousePrefab = _housePrefabs[Random.Range(0, _housePrefabs.Length)];
            Vector3 position = Vector3.zero;

            GameObject newHouseObject = Instantiate(newHousePrefab);

            position = new Vector3(referencePosition.x, 
                (referencePosition.y - referenceHouse.GetComponent<Collider2D>().bounds.size.y / 2) - (newHouseObject.GetComponent<Collider2D>().bounds.size.y / 2) ,
                  referencePosition.z);
            newHouseObject.transform.position = position;
            //check if this house is hititng any other houses
            // Create a collider box that represents the new house's bounds
            Collider2D houseCollider = newHouseObject.GetComponent<Collider2D>();


            if (houseCollider != null)
            {
                // Check if the house is hitting any other objects with the "House" tag
                Collider2D[] colliders = Physics2D.OverlapBoxAll(houseCollider.bounds.center,
                    houseCollider.bounds.size, 0f, LayerMask.GetMask("House"));

                // If there are other colliders besides the current house's collider
                if (colliders.Length > 1)
                {
                    //destory this house and try agian
                    referencePosition = newHouseObject.transform.position;
                    Destroy(newHouseObject);
                   continue; //no more houses
                }
            }
            //if on the right side of the road
            if (position.x > FindClosestObjectWithTag("Road").transform.position.x)
            {
                //flip the object on x axis
                newHouseObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            //recolor the two children of the object
            Color one = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
            Color two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
            while (one == two)
            {
                two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
            }
            newHouseObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = one;
            newHouseObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = two;
            HouseInfoStorage.AddHouse(newHouseObject, newHousePrefab);
            referencePosition = newHouseObject.transform.position;
            referenceHouse = newHouseObject;
            //spawn a bin
        }
    }

    void SpawnHousesAbove(GameObject referenceHouse)
    {
        Vector3 referencePosition = referenceHouse.transform.position;
        //add 3 to init y pos
        referencePosition.y += 3;
        for (int i = 0; i < _numberOfHouses; i++)
        {
            GameObject newHousePrefab = _housePrefabs[Random.Range(0, _housePrefabs.Length)];
            Vector3 position = Vector3.zero;

            GameObject newHouseObject = Instantiate(newHousePrefab);
          
            position = new Vector3(referencePosition.x,
                (referencePosition.y + referenceHouse.GetComponent<Collider2D>().bounds.size.y / 2) + (newHouseObject.GetComponent<Collider2D>().bounds.size.y / 2),
                  referencePosition.z);
            newHouseObject.transform.position = position;

            //check if this house is hititng any other houses
            // Create a collider box that represents the new house's bounds
            Collider2D houseCollider = newHouseObject.GetComponent<Collider2D>();

            if (houseCollider != null)
            {
                // Check if the house is hitting any other objects with the "House" tag
                Collider2D[] colliders = Physics2D.OverlapBoxAll(houseCollider.bounds.center,
                    houseCollider.bounds.size, 0f, LayerMask.GetMask("House"));

                // If there are other colliders besides the current house's collider
                if (colliders.Length > 1)
                {

                    //destory this house and try agian
                    referencePosition = newHouseObject.transform.position;
                    Destroy(newHouseObject);
                    continue; //no more houses
                }
            }
            //if on the right side of the road
            if (position.x > FindClosestObjectWithTag("Road").transform.position.x)
            {
              newHouseObject.transform.localScale =  new Vector3(-1, 1, 1);
            }
            //recolor the two children of the object
            Color one = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
            Color two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
            while (one == two)
            {
                two = _acceptableColors[Random.Range(0, _acceptableColors.Length)];
            }
            newHouseObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = one;
            newHouseObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = two;
            HouseInfoStorage.AddHouse(newHouseObject, newHousePrefab);
            referencePosition = newHouseObject.transform.position;
            referenceHouse = newHouseObject;
            //spawn a bin
        }
    }

    void CleanUp()
    {
        //find roads or houses that are over 40 units away from the player and remove them
    }


    GameObject FindClosestObjectWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        //find the closest one
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(_playerTruck.transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }
        return closestObject;
    }

    

}
