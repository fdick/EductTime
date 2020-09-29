using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void startLevel1()
    {
        Application.LoadLevel(1);
    }

    public void startLevel2()
    {
        Application.LoadLevel(2);
    }
}

