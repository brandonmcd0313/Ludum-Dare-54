using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] string[] _tips;
    [SerializeField] GameObject _toolTipPrefab;
    [SerializeField] Vector3 _toolTipSpawnPos;
    static bool _ranTipsMain, _ranTipsBinToTruck;
    GameObject toolTip;

    // Start is called before the first frame update
    void Start()
    {
        
        //switch case for scene
        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            case "Main":
                if (!_ranTipsMain)
                {
                    StartCoroutine(RunTips());
                    _ranTipsMain = true;
                    //set to spawn pos
                    toolTip.transform.position = _toolTipSpawnPos;
                }
                else
                {
                    toolTip.transform.position = new Vector3(100,100,0);
                }
                break;
            case "BinToTruck":
                print("bin to truck");
                if (!_ranTipsBinToTruck)
                {
                    StartCoroutine(RunTips());
                    _ranTipsBinToTruck = true;
                    toolTip.transform.position = _toolTipSpawnPos;
                }
                else
                {
                    toolTip.transform.position = new Vector3(100, 100, 0);
                }
                break;
        }

    }

    IEnumerator RunTips()
    {
        GameObject truck = null;
        //find truck and disable move
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main")
        {
            truck = GameObject.FindGameObjectWithTag("Truck");
            if (truck != null)
            {
                truck.GetComponent<TruckDrivingBehaviour>().canMove = false;
            }
        }
        toolTip = Instantiate(_toolTipPrefab);
        //hook up text
        TextMeshPro text = toolTip.transform.GetChild(1).GetComponent<TextMeshPro>();
        foreach (string tip in _tips)
        {
            //set text
            text.text = tip;
            //wait for mouse button 0 to be pressed
           yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));

        }
        Destroy(toolTip);
        if (truck != null)
        {
            truck.GetComponent<TruckDrivingBehaviour>().canMove = true;
        }
    }

    public void StopToolTip()
    {
        StopAllCoroutines();
        Destroy(toolTip);
    }
}
