using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwap : MonoBehaviour
{
    bool _swappedScene = false;
    [SerializeField] AudioClip _leaveSceneSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_swappedScene)
        {
            _swappedScene = true;
            StartCoroutine(SwapScene());
        }
    }
    
    IEnumerator SwapScene()
    {
        //log all trash
        GameObject.Find("TrashLocationManager").GetComponent<TrashLocationManager>().LogTrashLocations();
        //play sound
        GetComponent<AudioSource>().PlayOneShot(_leaveSceneSound);
        
        //wait one sec
        yield return new WaitForSeconds(2);
        //load main scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
