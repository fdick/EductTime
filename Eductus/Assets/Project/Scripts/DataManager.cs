using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class DataManager : MonoBehaviour
{
    public List<ObjectData> data = new List<ObjectData>();
    public bool FileIsload = false;

    private static DataManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static DataManager Instance { get => instance; set => instance = value; }
}
