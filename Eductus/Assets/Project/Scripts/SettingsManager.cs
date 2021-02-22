using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager instance;

    public bool abbreviationNames;

    public static SettingsManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = "SettingManager";
                go.AddComponent<SettingsManager>();

                instance = go.GetComponent<SettingsManager>();
            }
            return instance;


        }
        set
        {
            instance = value;

        }
    }

    private void Awake()
    {

    }

   

}
