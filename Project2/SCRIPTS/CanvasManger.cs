using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManger : MonoBehaviour
{
    [SerializeField]
    private GameObject _fade;
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GlobalFunctions _G_func;
    [SerializeField]
    private DiabloController _player;
    [SerializeField] private AudioSource music;

    public void Exit() //Во время игры крестик -> pausePanel
    {
        Time.timeScale = 0;
        _fade.SetActive(true);
        _pausePanel.SetActive(true);
        music.Pause();

    }

    public void OffPausePanel() // Pause panel -> 
    {
        _pausePanel.SetActive(false);
        
        if (SceneManager.GetActiveScene().buildIndex == 2 && _player.live)
        {
            DiabloController.deathEvent -= _G_func.multi;
            DiabloController.deathEvent -= _G_func.multi_G;
        }
    }

    public void Exit2() // Скрывание pausePanel
    {
        music.UnPause();
        _pausePanel.SetActive(false);
        _fade.SetActive(false);
        Time.timeScale = 1;
    }
}
