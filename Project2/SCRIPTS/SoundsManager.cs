using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    AudioSource exit;
    AudioSource buy;
    AudioSource select;
    AudioSource lvlUp;

    void Start()
    {
        exit = transform.Find("exit_sound").GetComponent<AudioSource>();
        buy = transform.Find("buy_sound").GetComponent<AudioSource>();
        select = transform.Find("select_sound").GetComponent<AudioSource>();
        lvlUp = transform.Find("lvlUp_sound").GetComponent<AudioSource>();
    }

    public void Exit()
    {
        exit.Play();
    }

    public void Buy()
    {
        buy.Play();
    }
    public void Select()
    {
        select.Play();
    }

    public void LvlUp()
    {
        lvlUp.Play();
    }

}
