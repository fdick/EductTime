using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    
    public  int currentGold;
    public  int awardGold;
    public  int minutes;
    public  int seconds;
    public  int revival;
    public  int stamina;
    //xpManager________________________________________________
    public int awardValue;
    public int Xp;
    public int levelNum;
    public int inBetweenValue;
    public int maxValue;
    public float fillAmount;
    public int ArenaRecord;
    public bool ads; 
    public bool firstPlay; 
    public int timesToShowAds;
   // public bool[] levelComplete = new bool[4];

    public bool levelComplete1;
    public bool levelComplete2;
    public bool levelComplete3;
    public bool levelComplete4;
    //public static float[] records = new float[4];

    public float recrods1;
    public float recrods2;
    public float recrods3;
    public float recrods4;

    public Data()
    {
        currentGold = Gold_manager.currentGold;
       // awardGold = Gold_manager.awardGold;
        minutes = Gold_manager.minutes;
        seconds = Gold_manager.seconds; 
        revival = Gold_manager.revival;
        stamina = Gold_manager.stamina;
        //______________________________________________

       // awardValue = Xp_manager.awardValue;
        Xp = Xp_manager.Xp;
        levelNum = Xp_manager.levelNum;
        inBetweenValue = Xp_manager.inBetweenValue;
        maxValue = Xp_manager.maxValue;
        fillAmount = Xp_manager.fillAmount;
        ArenaRecord = Xp_manager.ArenaRecord;
        ads = Xp_manager.ads;
        firstPlay = Xp_manager.firstTime;
        timesToShowAds = Xp_manager.timesToShowAds;
        //for (int i = 0; i < 4; i++)
        //{
        //    levelComplete[i] = LevelComplete.boole[i];
        //}

        
        levelComplete1 = LevelComplete.boole[0];
        levelComplete2 = LevelComplete.boole[1];
        levelComplete3 = LevelComplete.boole[2];
        levelComplete4 = LevelComplete.boole[3];

        //for (int i = 0; i < 4; i++)
        //{
        //    records[i] = Xp_manager.records[i];
        //}

        recrods1 = Xp_manager.records[0];
        recrods2 = Xp_manager.records[1];
        recrods3 = Xp_manager.records[2];
        recrods4 = Xp_manager.records[3];
    }

}