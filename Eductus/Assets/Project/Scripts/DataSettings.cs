using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSettings 
{
    public bool _abbreviationNames;

    public DataSettings()
    {
        _abbreviationNames = SettingsManager.Instance.abbreviationNames;
    }

}
