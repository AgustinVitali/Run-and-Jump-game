using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{

    public MapPoint up, right, down, left;  

    public bool isLevel, isLocked;

    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, totalGems;
    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;


    

    // Start is called before the first frame update


    void Start()
    {
        if (isLevel && levelToLoad != null)
        {
            // Default to locked
            isLocked = true;
            
            // Check if this level should be unlocked
            if (levelToLoad == levelToCheck || PlayerPrefs.GetInt(levelToLoad + "_unlocked") == 1)
            {
                isLocked = false;
                
            }
            else
            {
                
            }

            // Load level stats
            gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems", 0);
            bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time", 0f);

            // Update badges
            if (gemsCollected >= totalGems && totalGems != 0)
            {
                gemBadge.SetActive(true);
            }
            if (bestTime <= targetTime && bestTime != 0) 
            {
                timeBadge.SetActive(true);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
