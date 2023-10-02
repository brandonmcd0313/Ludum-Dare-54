using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNight : MonoBehaviour
{
    public GameObject overlay;
    static float SecondsPerDay = 120;
    static float SecondsPassed = 0;
    public static bool runningDay = false;
  
    // Start is called before the first frame update
    void Start()
    {
        //get overlay from the scene
        overlay = GameObject.Find("Overlay");
        overlay.SetActive(true);
        //if this not the menu scene run the day
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            runningDay = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        print("ran update on daynight");
        if (!runningDay)
        {
            return;
        }
        
        if (SecondsPassed < SecondsPerDay)
        {
            print((int)SecondsPassed);
            int i = (int)(115 - 494 * (SecondsPassed / SecondsPerDay) + (571 * Mathf.Pow(SecondsPassed / SecondsPerDay, 2)));
            SecondsPassed += Time.deltaTime;
            GameObject.Find("Overlay").GetComponent<Image>().color =
                    new Color(0, 0, 0, i / 255f);

        }
        else
        {
            print("end of day");
            SecondsPassed = 0;
            SceneManager.LoadScene("EndOfDay");
        }
    }
    
}
