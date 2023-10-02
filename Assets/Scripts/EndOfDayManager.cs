using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VersionControl.Git;
using UnityEngine.SceneManagement;

public class EndOfDayManager : MonoBehaviour
{
    int howmuchtrashwegot;
    public TMP_Text eodresults, binscollectedtoday, binscollectedtotal, totalpiecesoftrashcollectedtoday, trashcollectedtotal;
    static int totalTrash;
    static int totalBins;
    static int dayCount = 0;
    public void Start()
    {
        DayNight.runningDay = false;
        dayCount++;
        //the maount of bins = the amount of houses that have been visited
        int binsToday = HouseInfoStorage.GetNumberOfVisitedHouses();
        int trashToday = TrashLocationManager.GetTrashCount();
        totalBins += binsToday;
        totalTrash += trashToday;
        //clear the house list
        HouseInfoStorage.ClearHouseList();
        TrashLocationManager.Clear();
        //text
        eodresults.text = "End Of Day "+ dayCount + " Results" ;
        
        binscollectedtoday.text = "Bins Collected Today: " + binsToday;
        binscollectedtotal.text = "Bins Collected Total: " + totalBins;
        totalpiecesoftrashcollectedtoday.text = "Trash Collected Today: " + trashToday;
        trashcollectedtotal.text = "Trash Collected Total: " + totalTrash;
        if (dayCount == 3)
        {
            eodresults.text = "End Of Game Results";
            totalpiecesoftrashcollectedtoday.text = "";
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && dayCount < 3)
        {
            //reload main scene
            DayNight.runningDay = true;
            SceneManager.LoadScene("Main");
        }
    }
    
}
