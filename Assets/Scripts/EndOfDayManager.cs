using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfDayManager : MonoBehaviour
{
    public void Start()
    {
        //clear the house list
        HouseInfoStorage.ClearHouseList();
    }
}
