using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInfoStorage : MonoBehaviour
{
    public static List<GameObject> Houses = new List<GameObject>();
    public static List<Vector3> HousePositions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        //spawn all houses at their positions
        for (int i = 0; i < Houses.Count; i++)
        {
            GameObject house = Instantiate(Houses[i]);
            house.transform.position = HousePositions[i];
        }
    }

    // Update is called once per frame
   public  static void ClearHouseList()
    {
        Houses.Clear();
        HousePositions.Clear();
    }
}

