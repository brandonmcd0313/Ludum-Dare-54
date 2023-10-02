using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNight : MonoBehaviour
{
    public GameObject overlay;
    static float SecondsPerDay = 120;
    static float SecondsPassed;
    public static bool runningDay = false;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        SecondsPassed = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        //get overlay from the scene
        overlay = GameObject.Find("Overlay");
        overlay.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(!runningDay)
        {
            return;
        }
        
        if (SecondsPassed < SecondsPerDay)
        {
            int i = (int)(115 - 494 * (SecondsPassed / SecondsPerDay) + (571 * Mathf.Pow(SecondsPassed / SecondsPerDay, 2)));
            SecondsPassed += Time.deltaTime;
            GameObject.Find("Overlay").GetComponent<Image>().color =
                    new Color(0, 0, 0, i / 255f);

        }
        else
        {
            SecondsPassed = 0;
            SceneManager.LoadScene("EndOfDay");
        }
    }
    
}
