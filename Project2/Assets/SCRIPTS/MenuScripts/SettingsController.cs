using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject manual;
    [SerializeField] private GameObject areYouSure;


    public void ExitFromMain()
    {
        gameObject.SetActive(false);
    }
    public void Manual()
    {
        main.SetActive(false);
        manual.SetActive(true);

    }

    public void ExitFromManual()
    {
        manual.SetActive(false);
        main.SetActive(true);
    }
    public void Reset()
    {
        areYouSure.SetActive(true);
    }
   

    public void Yes()
    {
        print("RESET");
    }

    public void No()
    {
        areYouSure.SetActive(false);

    }
}
