using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionManager : MonoBehaviour
{
    public static bool isToTheRight = true;


    void setFacingDirection(bool isFacingRight)
    {
        isToTheRight = isFacingRight;
    
    }
}
