using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNight : MonoBehaviour
{
    public GameObject overlay;
    // Start is called before the first frame update
    void Start()
    {
        overlay.SetActive(false);
        StartCoroutine(FadeAndLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator FadeAndLoad()
    {
        //fade to black
        overlay.SetActive(true);
        for(int i = 0; i <= 255f; i += 5)
        {
            overlay.GetComponent<Image>().color =
                new Color(0, 0, 0, i / 255f);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
