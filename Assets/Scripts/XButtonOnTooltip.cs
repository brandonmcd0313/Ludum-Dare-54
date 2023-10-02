using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XButtonOnTooltip : MonoBehaviour
{

    private void OnMouseDown()
    {
        //stop tooltip
        GameObject.Find("ToolTipManager").GetComponent<TooltipManager>().StopToolTip();
    }
}
