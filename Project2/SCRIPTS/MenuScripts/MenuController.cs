using UnityEngine;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using System.IO;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadingPanel;
    [SerializeField]
    private Image _imageFade;
    [SerializeField]
    private GameObject _playPanel;
    [SerializeField]
    private GameObject _choicePanel;
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private GameObject _recTextS;
    [SerializeField]
    private GameObject _recordPanel;
    private Text[] nameRec; // имя челика из рекордов
    private Text[] scoreRec; // очки челика из рекордов
    private bool load = false;

    int youIndex = -1;
    [SerializeField] private DataRecord[] dataRecord = new DataRecord[4];

    private readonly string[] disable_networks = { "facebook", "flurry", "pubnative", "inmobi" };
    int timesToTriedToShow = 0;

    [SerializeField]
    private GameObject[] particles;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject appodealPanel;
    

    const string  appKey = "32b8fa9fe66084e70af5be1f78ad388632c0dddeb587968c";
    void Awake()
    {
        Time.timeScale = 1;
        print("ads" + Xp_manager.ads);
        if (!File.Exists(Application.persistentDataPath + "/Data.d"))
            SaveData();
        print("adsBeforeSave " + Xp_manager.ads);
        if (Xp_manager.firstMinute)
        {
            LoadData();
            Xp_manager.firstMinute = false;

        }
        print("adsLoad " + Xp_manager.ads);

        if (Xp_manager.records[0] != 0)
        {
            BecomeRecords(dataRecord[0], 0);
        }
         if (Xp_manager.records[1] != 0)
        {
            BecomeRecords(dataRecord[1], 1);
        }
         if (Xp_manager.records[2] != 0)
        {
            BecomeRecords(dataRecord[2], 2);
        }
         if (Xp_manager.records[3] != 0)
        {
            BecomeRecords(dataRecord[3], 3);
        }


        if (Xp_manager.firstTime)
        {
            appodealPanel.SetActive(true);
        }
        nameRec = new Text[10];
        for (int i = 0; i < nameRec.Length; i++)  // Получение компонента текст
        {
            nameRec[i] = _recTextS.transform.GetChild(i).GetComponent<Text>();
        }

        scoreRec = new Text[10];
        for (int i = 10; i < scoreRec.Length + 10; i++) // Получение компонента текст
        {
            scoreRec[i-10] = _recTextS.transform.GetChild(i).GetComponent<Text>();
        }

     

    }

    void Start()
    {

         InitAds(); //Инициализируем рекламу
      


    }
    //ADS_____________________________________________________________________________________________________

    void InitAds()
    {
       // Appodeal.setTesting(true);
        Appodeal.muteVideosIfCallsMuted(true);

        foreach (string nw in disable_networks)
        {
            Appodeal.disableNetwork(nw);
        }

        Appodeal.initialize(appKey, Appodeal.BANNER_TOP | Appodeal.INTERSTITIAL, Xp_manager.ads);
        Appodeal.disableLocationPermissionCheck();
      
    }


    public void ShowInterstitial()
    {
        timesToTriedToShow++;
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && timesToTriedToShow >=2)
        {
            timesToTriedToShow = 0;
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
      
    }

    public void ShowBannerTop()
    {
        if (Appodeal.isLoaded(Appodeal.BANNER_TOP))
            Appodeal.showBannerView(Appodeal.BANNER_TOP, Appodeal.BANNER_HORIZONTAL_LEFT, "top");
       
    }

    public void HideBannerTop()
    {
        Appodeal.hideBannerView();
    }


    //ADS_____________________________________________________________________________________________________


    public void SaveData()
    {
        print("BeforeSave FirstTime : " + Xp_manager.firstTime);
        SaveLoadManager.Save();
        print("SAVE FirstTime " + Xp_manager.firstTime);
    }

    public void LoadData()
    {

        Data data = SaveLoadManager.Load();

        Gold_manager.currentGold = data.currentGold;
       // Gold_manager.awardGold = data.awardGold;
        Gold_manager.minutes = data.minutes;
        Gold_manager.seconds = data.seconds;
        Gold_manager.revival = data.revival;
        Gold_manager.stamina = data.stamina;
        //______________________________________________

       // Xp_manager.awardValue = data.awardValue;
        Xp_manager.Xp = data.Xp;
        Xp_manager.levelNum = data.levelNum;
        Xp_manager.inBetweenValue = data.inBetweenValue;
        Xp_manager.maxValue = data.maxValue;
        Xp_manager.fillAmount = data.fillAmount;
        Xp_manager.ArenaRecord = data.ArenaRecord;
        Xp_manager.ads = data.ads;
        Xp_manager.firstTime = data.firstPlay;
        Xp_manager.timesToShowAds = data.timesToShowAds;
        //for (int i = 0; i < 4; i++) //Окончание лвла
        //{
        //    LevelComplete.boole[i] = data.levelComplete[i];
        //}

        LevelComplete.boole[0] = data.levelComplete1;
        LevelComplete.boole[1] = data.levelComplete2;
        LevelComplete.boole[2] = data.levelComplete3;
        LevelComplete.boole[3] = data.levelComplete4;

 
            Xp_manager.records[0] = data.recrods1;
            Xp_manager.records[1] = data.recrods2;
            Xp_manager.records[2] = data.recrods3;
            Xp_manager.records[3] = data.recrods4;
        


        print("LOAD FirstTime " + Xp_manager.firstTime);
    }


    void OnApplicationQuit()
    {
        SaveData();
       // load = false;


    }

    void OnApplicationPause()
    {
        SaveData();
    }


    void BecomeRecords(DataRecord dataRecord, int a) //Переделать рекорды после загрузки сохранения
    {
        var time = Xp_manager.records[a];

        for (int i = 0; i < dataRecord.names.Length; i++)  // Есть ли уже ваш рекорд в таблице
        {
            if (dataRecord.names[i] == "YOU")
            {
                youIndex = i;
                break;
            }
        }

        if (youIndex != -1 && time < dataRecord.scoresTime[youIndex])  // Если нынешний рекорд есть то ..  
        {                                                               // и если наше значение меньше чем наш прошлый рекорд

            for (int i = 0; i < dataRecord.scoresTime.Length; i++)
            {

                if (time < dataRecord.scoresTime[i]) // Находим значение кот. больше нашего минимально
                {
                    for (int q = youIndex; q > i; q--)  // Установка значений
                    {

                        dataRecord.scoresTime[q] = dataRecord.scoresTime[q - 1];
                    }
                    for (int q1 = youIndex; q1 > i; q1--)  // Установка имен
                    {

                        dataRecord.names[q1] = dataRecord.names[q1 - 1];
                    }
                    dataRecord.scoresTime[i] = time;
                    dataRecord.names[i] = "YOU";
                    break;
                }

            }
        }
        else if (youIndex == -1)  // Если нынешнего рекорда нет, то поставить его
        {
            for (int i = 0; i < dataRecord.scoresTime.Length; i++)
            {

                if (time <= dataRecord.scoresTime[i] || dataRecord.scoresTime[i] == 0)
                {
                    var i2 = i;
                    if (time != dataRecord.scoresTime[i])
                    {
                        for (int j = dataRecord.scoresTime.Length - 1; j > i2; j--) // Для значений
                        {

                            dataRecord.scoresTime[j] = dataRecord.scoresTime[j - 1];

                        }

                        for (int j = dataRecord.names.Length - 1; j > i2; j--) // Для имени
                        {

                            dataRecord.names[j] = dataRecord.names[j - 1];

                        }
                        dataRecord.scoresTime[i] = time;
                        dataRecord.names[i] = "YOU";
                    }

                    break;
                }
            }
        }
    } // Выставить рекрды в меню
 // Выставить рекрды в меню

    public void Play()
    {
        OffParcticles();
        _playPanel.gameObject.SetActive(true); //Включить выбор режимов игры
        FadeOn();

    }

    public void Loading()
    {
     //Начать визуальную загрузку
        _loadingPanel.SetActive(true);
    }

    public void Quit()
    {
       
        Application.Quit();

    }

    public void localRecord(DataRecord data)
    {
        _choicePanel.SetActive(false);
        _recordPanel.SetActive(true);

        for (int i = 0; i < nameRec.Length; i++)  // Установка имен
        {
            nameRec[i].text = data.names[i];
            if (nameRec[i].text == "YOU")    // Сделать имя "YOU" красным цветом
            {
                nameRec[i].color = Color.red;

            }
            else
            {
                nameRec[i].color = Color.grey;
               
            }

        }

        for (int i = 0; i < scoreRec.Length; i++) // Установка времени(очки)
        {
            scoreRec[i].text = data.scoresTime[i].ToString();
        }


        

        _recTextS.transform.Find("levelNumber").GetComponent<Text>().text = data.levelNumber.ToString(); //Установка номера лвла


        Debug.Log("record");
    }

    public void Shop()
    {

        
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    //____________________________________________________________________________

    public void Company()
    {
        _choicePanel.SetActive(true);

    }

    public void Exit(GameObject go)  // Крестик
    {
        OnParcticles();
        FadeOff();
        go.SetActive(false);
    }

    public void Exit1(GameObject go)  // Крестик низкого уровня(без убирания фейда)
    {

        go.SetActive(false);
    }

    public void Exit2(GameObject go)  // Крестик низкого уровня(без убирания фейда)
    {
        transform.parent.gameObject.SetActive(false);
        go.SetActive(true);
    }

    public void ExitUniverse(GameObject go)
    {
        Exit(go);
        _playPanel.SetActive(false);
    }

    //____________________________________________________________________________

    public void FadeOn()
    {
        var a = new Color(0.12f, 0.12f, 0.12f, 0.8f);
        _imageFade.color = a;
        _imageFade.raycastTarget = true;
    }

    public void FadeOff()
    {
        var a = new Color(1f, 1f, 1f, 0);
        _imageFade.color = a;
        _imageFade.raycastTarget = false;
    }

    private void OffParcticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].gameObject.SetActive(false);
        }
    }

    private void OnParcticles()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].gameObject.SetActive(true);
        }
    }
}
