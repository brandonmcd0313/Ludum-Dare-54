using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] string[] _tips;
    [SerializeField] GameObject _toolTipPrefab;
    [SerializeField] Vector3 _toolTipSpawnPos;
    static bool _ranTipsMain, _ranTipsBinToTruck, _ranTipsEndOfDay;
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
                break;
            case "BinToTruck":
                if (!_ranTipsBinToTruck)
                {
                    StartCoroutine(RunTips());
                    _ranTipsBinToTruck = true;
                    toolTip.transform.position = _toolTipSpawnPos;
                }
                break;
            case "EndOfDay":
                if (!_ranTipsEndOfDay)
                {
                    StartCoroutine(RunTips());
                    _ranTipsEndOfDay = true;
                    toolTip.transform.position = _toolTipSpawnPos;
                }
                break;
        }

    }

    IEnumerator RunTips()
    {
        toolTip = Instantiate(_toolTipPrefab);
        //hook up text
        TextMeshPro text = toolTip.transform.GetChild(1).GetComponent<TextMeshPro>();
        foreach (string tip in _tips)
        {
            //set text
            text.text = tip;
            //wait for mouse button 0 to be pressed
           yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            
        }
    }

    public void StopToolTip()
    {
        StopAllCoroutines();
        Destroy(toolTip);
    }
}
