using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject _losePanel;
    [SerializeField]
    private GameObject fadePanel;
    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private Timer _timer;
    [SerializeField]
    private Animator _animatorTimer;
    [SerializeField]
    private GameObject _pauseButt;
    private int _arenaRecord;
    [SerializeField]
    private Text _recText;

    [SerializeField]
    private MetronomCtrl _metronom;
    [SerializeField]
    private int GoldCount;
    [SerializeField]
    private int XpCount;
    [SerializeField]
    private RevivePanel _revivePanel;

  




    void Awake()
    {
        DiabloController.deathEvent += multi_G; // Подписываем эту функцию на ивент
       
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            
            DiabloController.deathEvent += multi;
            _arenaRecord = Xp_manager.ArenaRecord;
            _recText.text = _arenaRecord.ToString();

        }      
   }
    public void multi_G()
    {
        _timer.loseORwin = true;
        if (_pauseButt != null)
        {
            _pauseButt.SetActive(false);
        }

        if (fadePanel != null)
        {
            fadePanel.SetActive(true);
        }
        if (_losePanel != null)
        {
            _losePanel.SetActive(true);
        }
       
        if (_animatorTimer != null)
        {
            _animatorTimer.Play("Timer");
        }

        if (buttons != null)
        {
            buttons.GetComponent<Animator>().Play("winPanelButtons");
        }

        if (fadePanel != null)
        {
            fadePanel.GetComponent<Animator>().Play("fadeLocal");
        }

        if (_revivePanel != null)
        {
            _revivePanel.OnRevivePanel(); // Включить минимагазин
        }
       

        DiabloController.deathEvent -= multi_G; // Отписываемся от ивента


    }

   


    public void multi()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {

            if (_metronom.GetMetros() <= _arenaRecord / 3 && _losePanel.transform.Find("TextBAD").gameObject != null)
            {
                _losePanel.transform.Find("TextBAD").gameObject.SetActive(true);
            }
            else if (_metronom.GetMetros() <= _arenaRecord / 2)
            {
                _losePanel.transform.Find("TextNOTBAD").gameObject.SetActive(true);
            }
            else if (_metronom.GetMetros() <= _arenaRecord)
            {
                _losePanel.transform.Find("TextGOOD").gameObject.SetActive(true);
            }
            else if (_metronom.GetMetros() > _arenaRecord && _losePanel.transform.Find("TextNEWRECORD").gameObject != null)
            {
                _losePanel.transform.Find("TextNEWRECORD").gameObject.SetActive(true);
                Xp_manager.ArenaRecord = _metronom.GetMetros();
            }
            CalculateGOLD();
            CalculateXP();
            Xp_manager.awardValue += XpCount;
            Gold_manager.awardGold += GoldCount;
            DiabloController.deathEvent -= multi;

        }
        
    }

    private void CalculateXP()
    {
        int m = _metronom.GetMetros();
        if (m <=222) //Easy level
        {
            XpCount = (int)(m * 3.5f);
        }
        if (m > 222 && m <= 800) // Medium level
        {
            int p1 = (int)(222 * 3.5f);
            int p2 = (int)((m - 222) * 20.5f) ;
            XpCount = p1 + p2;

        }
        if (m>800) // Hard level
        {
            int p1 = (int)(222 * 3.5f);
            int p2 = (int)(500 * 9.5f);
            int p3 = (m - 222 - 500) * 222;
            XpCount = p1 + p2 + p3;
        }
       
    }
    private void CalculateGOLD()
    {
        int m = _metronom.GetMetros();
        if (m <= 222) //Easy level
        {
            GoldCount = (int)(m * 1.5f);
        }
        if (m > 222 && m <= 800) // Medium level
        {
            int p1 = (int)(222 * 3.5f);
            int p2 = (int)((m - 222) * 8.5f);
            GoldCount = p1 + p2;

        }
        if (m > 800) // Hard level
        {
            int p1 = (int)(222 * 3.5f);
            int p2 = (int)(500 * 9.5f);
            int p3 = (m - 300 - 500) * 50;
            GoldCount = p1 + p2 + p3;
        }
    }

}
