using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(DirectionManager.isToTheRight)
        {
            //localScale normal

            //rotate y by 180
            transform.RotateAround(transform.position, transform.up, 180f);
        }
        else
        {
        }
    }
    
}
