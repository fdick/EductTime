using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.IO;
using System.IO;

public class Ads : MonoBehaviour
{
    void Start()
    {
        //SaveLoadManager.Save();
      
        if (!Xp_manager.firstTime)
        {
            gameObject.SetActive(false);
        }
    }
    
  
  public void YesButton()
    {
        print("ads" + Xp_manager.ads);
        Xp_manager.ads = true;
        Xp_manager.firstTime = false;
        gameObject.SetActive(false);
        print("ads" + Xp_manager.ads);
    }

    public void NoThanks()
    {
        Xp_manager.ads = false;
        Xp_manager.firstTime = false;
        gameObject.SetActive(false);

    }

    public void onPLClick()
    {
        Application.OpenURL("https://www.appodeal.com/privacy-policy");
    }
}
