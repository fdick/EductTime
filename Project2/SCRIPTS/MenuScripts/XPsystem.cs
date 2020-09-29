using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPsystem : MonoBehaviour
{
    [SerializeField]
    private int _inBetweenValue; // Поинты полоски экспы
    [SerializeField]
    private int maxBetweenValue;
    [SerializeField]
    private float speed;

    private float _fillAmount;

    private int _level = 1; // номер lvl'а
    [SerializeField]
    private Text _levelTextVisual; // Визуальная текст лвл'а
    [SerializeField]
    private Image _foreGround;


    public int awardXp; // Награда за прохождение
    [SerializeField]
    private int Xp;
    [SerializeField]
    private int izok; // Избыток


    [SerializeField]
    private GameObject _levelUP;
    public AudioSource lvlUp_sound;
    

    [SerializeField]
    Animator _animator;
    [SerializeField]
    Animator _aPlusXP;
    [SerializeField]
    Text _plusXPtext;







    public int GetLevel()  // GET
    {
        return _level;
    }

    public void SetLevel(int v)  //SET
    {
        _level = v;
    }

    public int GetBetweenValue() // GET
    {
        return _inBetweenValue;
    }

    public void SetBetweenValue(int v)  //SET
    {
        _inBetweenValue = v;
    }

     void Start()
    {
        GetOutManager();
        AwardValue(awardXp);
       // SaveInManager();


    }
    void OnDisable()
    {
        SaveInManager();
    }

    public void AwardValue(int award) // Передаем наградный опыт
    {
         // Получили некое количество опыта
      
        if (award > 0)
        {
            _aPlusXP.Play("+XP");
            _plusXPtext.text = "+" + award.ToString();
        }
        
        if (_inBetweenValue + awardXp > maxBetweenValue)
        {
            izok = (_inBetweenValue + awardXp) - maxBetweenValue; // find избыток
            _inBetweenValue = maxBetweenValue + 1;
        

        }
        else
        {
            _inBetweenValue += awardXp;
            awardXp = 0;
            
           
        }


        Xp_manager.awardValue = 0;
    }

    void FixedUpdate()
    {
   
        Multiply();
        if (izok == 0)
        {
            SaveInManager();
        }

    }

    public void SaveInManager()
    {
        Xp_manager.levelNum = _level;
        Xp_manager.inBetweenValue = _inBetweenValue;
        Xp_manager.maxValue = maxBetweenValue;
        _fillAmount = _foreGround.fillAmount;
        Xp_manager.fillAmount = _fillAmount;
      
    }

    public void GetOutManager()
    {
        if (Xp_manager.levelNum != 0)
        {
            _level = Xp_manager.levelNum;
            _levelTextVisual.text = GetLevel().ToString(); // Установка визуальной части
            //Xp_manager.levelNum = 0;
        }
        if (Xp_manager.inBetweenValue != 0)
        {
            _inBetweenValue = Xp_manager.inBetweenValue;
           // Xp_manager.inBetweenValue = 0;
        }
        if (Xp_manager.maxValue != 0)
        {
            maxBetweenValue = Xp_manager.maxValue;
          //  Xp_manager.maxValue = 0;
        }
        if (Xp_manager.fillAmount != 0)
        {
            _fillAmount = Xp_manager.fillAmount;
        }
        if (Xp_manager.awardValue != 0)
        {
            awardXp = Xp_manager.awardValue;
            Xp_manager.awardValue = 0;
        }
   
       
    }

    void ShowLevelUpPanel()
    {
        lvlUp_sound.Play();
        _levelUP.SetActive(true);
        StartCoroutine(closeLevelUpPanel());
    }

    //_______________________________________________________________

    public IEnumerator closeLevelUpPanel()
    {
        yield return new WaitForSeconds(5);
        _levelUP.SetActive(false);
    }


    void LVLup()
    {
        if (_foreGround.fillAmount >= 1)
        {
            //SaveInManager();
            _animator.Play("levelUp");
            ShowLevelUpPanel();
            _level++;
            _foreGround.fillAmount = 0;
            _inBetweenValue = 0;


            if (_level < 15)
            {
                maxBetweenValue = maxBetweenValue + _level * 1200;
            }
            if (_level >= 15 && _level < 20)
            {
                maxBetweenValue = maxBetweenValue + _level * 3000;
            }



            if (izok > 0)
            {
                _inBetweenValue = izok;

                if (_inBetweenValue > maxBetweenValue)
                {
                    izok = _inBetweenValue - maxBetweenValue;
                   
                }
                else izok = 0;
            }
   


            SetLevel(_level);
            _levelTextVisual.text = GetLevel().ToString(); // Установка визуальной части
        }
    }


    void Multiply()
    {
        if (_fillAmount != 0)
        {
            _foreGround.fillAmount = _fillAmount;
            _fillAmount = 0;
        }
        LVLup();
        float t = (float)_inBetweenValue / maxBetweenValue;
        var v = Mathf.Lerp(_foreGround.fillAmount, t, speed * Time.deltaTime);


        _foreGround.fillAmount = v;
       

    }
}
