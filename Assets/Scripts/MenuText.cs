using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MenuText : MonoBehaviour
{
    public TMP_Text guitext;
    public GameObject PlayButton;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.GetComponent<Button>();
        PlayButton.SetActive(true);
        guitext.text = "Welcome To Garbage Sim The Best\t\t\t\t   Game Ever";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGameButton()
    {
        SceneManager.LoadScene("Main");
    }

}
