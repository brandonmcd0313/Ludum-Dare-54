using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInfoStorage : MonoBehaviour
{
    public static List<GameObject> HousePrefabRefrences = new List<GameObject>();
    public static List<Sprite> HousesA = new List<Sprite>();
    public static List<Sprite> HousesB = new List<Sprite>();
    public static List<Sprite> HousesC = new List<Sprite>();
    public static List<Vector3> HousePositions = new List<Vector3>();
    public static List<bool> HasHouseBeenVisited = new List<bool>();
    public static Vector3 PlayerPosition;
    static bool firstLoad = true;
    // Start is called before the first frame update
    void Start()
    {
        //if first load then do  not run stuff
        if (firstLoad)
        {
            firstLoad = false;
            return;
        }
        //if player pos not set then set it
        if (PlayerPosition == Vector3.zero)
        {
            PlayerPosition = new Vector3(3.28f, 0.31f, 0f);
        }
        else
        {
            GameObject truck = GameObject.FindGameObjectWithTag("Truck");
            truck.transform.position = new Vector3(truck.transform.position.x, PlayerPosition.y, 0);
        }
        //spawn all houses at their positions
        if (HousesA.Count == 0)
        {
            return;
        }
        for (int i = 0; i < HousesA.Count; i++)
        {
            SpawnHouse(i);
        }
    }

    // Update is called once per frame
   public  static void ClearHouseList()
    {
        HousesA.Clear();
        HousesB.Clear();
        HousesC.Clear();
        HousePositions.Clear();
        HousePrefabRefrences.Clear();
        HasHouseBeenVisited.Clear();
        PlayerPosition = new Vector3(3.28f, 0.31f, 0f);
        firstLoad = true;
    }

    public static void AddHouse(GameObject house, GameObject prefabRefrence)
    {
        //store all the sprites
        HousesA.Add(house.GetComponent<SpriteRenderer>().sprite);
        HousesB.Add(house.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        HousesC.Add(house.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite);
        //store all the positions
        HousePositions.Add(house.transform.position);
        //store the prefab refrence
        HousePrefabRefrences.Add(prefabRefrence);
        //store if the house has been visited
        HasHouseBeenVisited.Add(false);
    }

    public static void SpawnHouse(int index)
    {
        print("spawned saved house");
        GameObject newHouse = Instantiate(HousePrefabRefrences[index]);
        
        //set all the sprites
        newHouse.GetComponent<SpriteRenderer>().sprite = HousesA[index];
        newHouse.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = HousesB[index];
        newHouse.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = HousesC[index];
        //set all the positions
        newHouse.transform.position = HousePositions[index];
        //tell the parent house it has been visited
        newHouse.GetComponent<House>().HasBeenVisited = HasHouseBeenVisited[index];
        //if on right side of road then flip it
        if (newHouse.transform.position.x > FindClosestObjectWithTag("Road", newHouse).transform.position.x)
        {
            newHouse.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public static void SetAsVisitedHouseAt(Vector3 position)
    {
        int closestHouseIndex = 0;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < HousePositions.Count; i++)
        {
            if (Vector3.Distance(position, HousePositions[i]) < closestDistance)
            {
                closestDistance = Vector3.Distance(position, HousePositions[i]);
                closestHouseIndex = i;
            }
           
        }
        HasHouseBeenVisited[closestHouseIndex] = true;
    }

   static  GameObject FindClosestObjectWithTag(string tag, GameObject refrence)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        //find the closest one
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(refrence.transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }
        return closestObject;
    }

    public static int GetNumberOfVisitedHouses()
    {
        int numberOfVisitedHouses = 0;
        for (int i = 0; i < HasHouseBeenVisited.Count; i++)
        {
            if (HasHouseBeenVisited[i])
            {
                numberOfVisitedHouses++;
            }
        }
        return numberOfVisitedHouses;
    }
}




