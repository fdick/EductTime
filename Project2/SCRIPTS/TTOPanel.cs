using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTOPanel : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private DiabloController diabloCtrl;

    public void onLive()
    {
        diabloCtrl.live = true;
    }

    public void onLoseORWin()
    {
        timer.loseORwin = false;
        diabloCtrl.enabled = true;
        gameObject.SetActive(false);
    }
}
