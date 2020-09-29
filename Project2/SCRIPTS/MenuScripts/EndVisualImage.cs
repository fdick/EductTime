using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndVisualImage : MonoBehaviour //Если уровень пройден , то перекрасить его в легкий зеленый цвет
{
   
    [SerializeField] private int indexOfLvl; 
    [SerializeField] private Color _color;
 

    void Awake()
    {

        if (LevelComplete.boole[indexOfLvl])
        {
            print(LevelComplete.boole[indexOfLvl]);
            gameObject.GetComponent<Image>().color = _color;
        }
    }
}
