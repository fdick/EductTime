using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>: MonoBehaviour where T: MonoBehaviour
{


    private static T _instance;
    //private static VirtualInputManager _instance2;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<T>();
                go.name = typeof(T).ToString();
            }

            return _instance;
        }
    }
}

