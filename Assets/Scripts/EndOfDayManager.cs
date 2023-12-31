using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndOfDayManager : MonoBehaviour
{
    public TMP_Text eodresults, binscollectedtoday, binscollectedtotal, totalpiecesoftrashcollectedtoday, trashcollectedtotal;
    static int totalTrash;
    static int totalBins;
   public static int dayCount = 0;
    
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
        if(dayCount == 1)
        {

            eodresults.text = "End Of Day One Results";
        }
        else if (dayCount == 2)
        {
            eodresults.text = "End Of Day Two Results";
        }
        binscollectedtoday.text = "Bins Visited Today: " + binsToday;
        binscollectedtotal.text = "Bins Visited Total: " + totalBins;
        totalpiecesoftrashcollectedtoday.text = "Trash Collected Today: " + trashToday;
        trashcollectedtotal.text = "Trash Collected Total: " + totalTrash;
        
        if (dayCount >= 4)
        {
            eodresults.text = "End Of Game Results";
            totalpiecesoftrashcollectedtoday.text = "";
            binscollectedtoday.text = "";


        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dayCount < 3)
        {
            //reload main scene
            DayNight.runningDay = true;
            SceneManager.LoadScene("Main");
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            //reload scene
            SceneManager.LoadScene("EndOfDay");
        }
    }
    
}
