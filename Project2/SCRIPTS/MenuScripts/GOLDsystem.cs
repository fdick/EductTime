using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOLDsystem : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Animator _aPlusGold;
    [SerializeField]
    private Text _PlusGOLDtext;
   

    [SerializeField]
    private Text visualiseText;

    public AudioSource money_sound;

    public int gold = 0;
    public int awardGold;

    void Start()
    {
        GetValue();
        AddGold();
        SetValue();
    }



   public void GetValue()
    {
        if (Gold_manager.awardGold != 0)
        {
            awardGold = Gold_manager.awardGold;
            visualiseText.text = gold.ToString();
            Gold_manager.awardGold = 0;
        }
        if (Gold_manager.currentGold != 0)
        {
            gold = Gold_manager.currentGold;
            visualiseText.text = gold.ToString();
          //  Gold_manager.currentGold = 0;
        }
       
        
    }

    public void SetValue()
    {
        Gold_manager.currentGold = gold;
        
    }

    public void AddGold()
    {
        gold += awardGold;
        if (awardGold != 0)
        {
            money_sound.Play();
            _animator.Play("goldUp");
            _aPlusGold.Play("+GOLD");
            _PlusGOLDtext.text = "+" + awardGold.ToString();
        }
        awardGold = 0;
        visualiseText.text = gold.ToString();
        SetValue();
    }

    public void MinusGold()
    {
        gold += awardGold;
        if (awardGold != 0)
        {
            _animator.Play("goldUp");
            _aPlusGold.Play("+GOLD");
            _PlusGOLDtext.text =  awardGold.ToString();
        }
        awardGold = 0;
        visualiseText.text = gold.ToString();
        SetValue();
    }
  
}
