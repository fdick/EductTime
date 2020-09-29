using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Xp_manager : MonoBehaviour
{

    public static int awardValue;
    public static int Xp;


    public static int levelNum;
    public static int inBetweenValue;
    public static int maxValue;
    public static float fillAmount;

    public static int ArenaRecord;
    public static bool ads; // Согласие на передачу личных данных апподилу
    
    public static bool firstTime = true; // Играю в первый раз
    public static int timesToShowAds;

    public static float[] records = new float[4];

    public static bool firstMinute = true;

    void OnApplicationPause(bool isPause)
    {
        if (!isPause)
        {
            //Time.timeScale = 1;
            Xp_manager.firstMinute = false;
        }
        else
        {
           // Time.timeScale = 0;
            SaveLoadManager.Save();
            Xp_manager.firstMinute = true;
        }
       
    }

    void OnApplicationQuit()
    {
        Xp_manager.firstMinute = true;
    }
 
}
