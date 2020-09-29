using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class RevivePanel : MonoBehaviour, IRewardedVideoAdListener
{
    [SerializeField]
    private GameObject _yesBtn;
    [SerializeField]
    private GameObject _watchVideoBtn;
    [SerializeField]
    private Text countOfRevive;

    [SerializeField]
    private CheckPointSystem _checkPointSystem;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private GameObject fade;
    [SerializeField]
    private GameObject deathPanel;
    [SerializeField]
    private GameObject _exitGlobal;
    [SerializeField]
    private Animator _timer;
    [SerializeField]
    private GameObject ttoPanel;
    [SerializeField]
    private GlobalFunctions globalFunc;

    [SerializeField]
    float speed;
    [SerializeField]
    float forceSpeed;
    [SerializeField]
    float defaultSpeed;
    [SerializeField] private AudioSource ticTac_sound;
    public bool revive = false;
    [SerializeField] private GameObject diablo; // подкат


    const string appKey = "32b8fa9fe66084e70af5be1f78ad388632c0dddeb587968c";

    void Start()
    {
        
        InitAds();
    }
    void InitAds()
    {


        Appodeal.initialize(appKey,Appodeal.REWARDED_VIDEO | Appodeal.INTERSTITIAL, Xp_manager.ads);
        Appodeal.setRewardedVideoCallbacks(this);

    }

    public void ShowVideo()
    {
        if(Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
            Appodeal.show(Appodeal.REWARDED_VIDEO);
    }

  

 

    public void YesButton() //Перекидываем игрока на ближайщий чек поинт
    {
        revive = true;
        
      



        ticTac_sound.Play();
        for (int i = 0; i < _checkPointSystem.boolCheckPoints.Length; i++)
        {
            if (_checkPointSystem.boolCheckPoints[i] == false) 
            {
                var posPoint = _checkPointSystem.checkPoints[i-1].transform.position;  
                _player.transform.position = new Vector3(posPoint.x, posPoint.y, posPoint.z + 2);
       
              
                break;
            }
            else if (_checkPointSystem.boolCheckPoints[_checkPointSystem.boolCheckPoints.Length -1] == true)
            {
                var posPoint = _checkPointSystem.checkPoints[_checkPointSystem.boolCheckPoints.Length -1].transform.position;
                _player.transform.position = new Vector3(posPoint.x, posPoint.y, posPoint.z+2);
               
        

                break;
            }
        }
        LivePlayer();
        Gold_manager.revival -= 1;
        if (Gold_manager.revival < 0)
            Gold_manager.revival = 0;
        DiabloController.deathEvent += globalFunc.multi_G;
        Exit();

        _player.GetComponent<DiabloController>().enabled = true;

    }
    public void WatchVideoButton()
    {
       ShowVideo();
    }
    public void Exit()
    {
        gameObject.SetActive(false);
    }

    private void LivePlayer()
    {
        ttoPanel.SetActive(true);
        ttoPanel.GetComponent<Animator>().Play("321");
        _timer.Play("New State");
        _exitGlobal.SetActive(true);
        losePanel.SetActive(false);
        deathPanel.SetActive(false);
        fade.SetActive(false);
        diablo.GetComponent<DiabloSwitchControllerToPodkat>().enabled = false;
        diablo.GetComponent<DiabloAnimation>().enabled = false;
        diablo.GetComponent<CharacterController>().enabled = false;
        diablo.transform.localPosition = new Vector3(0,0,0);
        _player.transform.GetChild(0).GetComponent<DiabloAnimation>()._animator.Play("Run");
        diablo.GetComponent<DiabloSwitchControllerToPodkat>().enabled = true;
        diablo.GetComponent<DiabloAnimation>().enabled = true;
        _player.GetComponent<CharacterController>().enabled = true;
        var p = _player.GetComponent<DiabloController>();
        p._speed = speed;
        p._defaultSpeed = defaultSpeed;
        p._forceSpeed = forceSpeed;
        _player.transform.GetChild(0).GetComponent<DiabloAnimation>()._animator.Play("Run");


    }


    public void OnRevivePanel() //Включить RevivePanel после смерти
    {
        for (int i = 0; i < _checkPointSystem.boolCheckPoints.Length; i++) // Включить эту панель , если игрок переcек хотя бы один чек поинт
        {
            if (_checkPointSystem.boolCheckPoints[i] == true)
            {
                
                gameObject.SetActive(true);
                countOfRevive.text = Gold_manager.revival.ToString(); // Поулчить значиние из менеджера

                if (Gold_manager.revival > 0) // Если есть токены воскрешения , то влючить кнопку воскрешения
                {
                    _yesBtn.SetActive(true);
                }
                else // Иначе включить кнопку просмотра рекламы
                {
                    _watchVideoBtn.SetActive(true);
                    _yesBtn.SetActive(false);
                }
              
        
                break;
            }
        }
    }

    public void onRewardedVideoLoaded(bool precache)
    {
       
    }

    public void onRewardedVideoFailedToLoad()
    {
       
    }

    public void onRewardedVideoShown()
    {
       
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        
    }

    public void onRewardedVideoClosed(bool finished)
    {
        if(finished)
            YesButton();
    }   

    public void onRewardedVideoExpired()
    {
        
    }

    public void onRewardedVideoClicked()
    {
       
    }

}
