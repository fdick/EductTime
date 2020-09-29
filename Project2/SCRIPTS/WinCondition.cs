using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField]
    private GameObject _winPanel;
    [SerializeField]
    private int level;

 
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private int GoldCount;
    [SerializeField]
    private int XpCount;
    [SerializeField]
    DataLvlCnvas lvlCanvas;
    [SerializeField]
    private GameObject fadePanel;
    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private GameObject PobedaPanel;
    [SerializeField]
    private Timer _timer;
    [SerializeField]
    private Animator _animatorTimer;
    [SerializeField]
    private DataRecord dataRecord;
    [SerializeField]
    private int youIndex = -1;
    [SerializeField]
    private GameObject _pauseBut;

    public AudioSource win_sound;
  

    void Awake()
    {
        GoldCount = XpCount = 0;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GoldCount = lvlCanvas.goldAward;
            XpCount = lvlCanvas.experienceAward;
            win_sound.Play();
            _timer.loseORwin = true; 
            multi(level);

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {

            Player.GetComponent<DiabloController>().enabled = false;

        }
    }


    void multi(int level)
    {
        StartCoroutine(Player.GetComponent<DiabloController>().muteMusic());
        _pauseBut.SetActive(false);
        fadePanel.SetActive(true);
        _winPanel.SetActive(true);
        PobedaPanel.SetActive(true);
        _animatorTimer.Play("Timer");
        buttons.GetComponent<Animator>().Play("winPanelButtons");
        
        fadePanel.GetComponent<Animator>().Play("fadeLocal");
      
     
            if (!LevelComplete.boole[level - 1])
            {
                Xp_manager.awardValue += XpCount;
                Gold_manager.awardGold += GoldCount;
                LevelComplete.boole[level - 1] = true;   // если true , то это значит,что этот уровень пройден.
            print("AWARDING1");
        }
        else // Если уровень уже пройден, то давать xp и gold в 3 раза меньше
        {
            Xp_manager.awardValue += XpCount / 3;
            Gold_manager.awardGold += GoldCount / 3;
            print("AWARDING2");

        }

        BecomeRecords();


    }

    void BecomeRecords()
    {

        var secondTime = (Mathf.Floor(_timer.secondTime)); //Перевод секунд в 0,04



        secondTime = secondTime / 100.0f;




        var time = _timer.minuteTime + secondTime; // Наш рекорд в одном числе
        if (level >=2)
        {
            Xp_manager.records[level - 2] = time;
        }
        

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
    
       
}
