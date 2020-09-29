using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateXPmanager : MonoBehaviour
{
    [SerializeField]
    private static bool created = false;
    [SerializeField]
    private GameObject XPmanager;
    [SerializeField]
    private GameObject GoldManger;
    void Awake()
    {
        if (!created)
        {
            Instantiate(XPmanager);
            Instantiate(GoldManger);
            created = true;
        }
    }

   
}
