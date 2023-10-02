using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndOfDayManager : MonoBehaviour
{
    int howmuchtrashwegot;
    public TMP_Text eodresults, binscollectedtoday, binscollectedtotal, totalpiecesoftrashcollectedtoday, trashcollectedtotal;
    public void Start()
    {
        //clear the house list
        HouseInfoStorage.ClearHouseList();

        //text
        eodresults.text = "End Of Day Results";
        binscollectedtoday.text = "Bins Collected Today:";
        binscollectedtotal.text = "Bins Collected Total:";
        totalpiecesoftrashcollectedtoday.text = "Trash Collected Today:";
        trashcollectedtotal.text = "Trash Collected Total:";
    }

}
