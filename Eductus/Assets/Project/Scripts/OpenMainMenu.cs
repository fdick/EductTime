using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.ModernUIPack;

public class OpenMainMenu : MonoBehaviour
{
    public Text text;
    public Animator mmAnimator;
    public GameObject humburger;
    public GameObject aboutProjectPAnel;
    public GameObject settingsPanel;
    public GameObject canceltrigger;
   
    public void OpenMenu()
    {
        mmAnimator.Play("Open");
        canceltrigger.SetActive(true);
    }
    public void CloseMenu()
    {
        mmAnimator.Play("close");
        humburger.GetComponent<AnimatedIconHandler>().isClicked = true;
        humburger.GetComponent<AnimatedIconHandler>().ClickEvent();

    }
    public void AboutProjectOpen()
    {
        aboutProjectPAnel.SetActive(true);
        text.text = "О проекте";
        CloseMenu();
    }
    public void AboutProjectClose()
    {
        aboutProjectPAnel.SetActive(false);
    }
    public void SettingsOpen()
    {
        settingsPanel.SetActive(true);
        text.text = "Настройки";
        CloseMenu();
    }
    public void SettingsClose()
    {
        settingsPanel.SetActive(false);
        SaveLoadManager.SaveSettings();
        CloseMenu();
    }

}
