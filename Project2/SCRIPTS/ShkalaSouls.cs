using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShkalaSouls : MonoBehaviour
{
   
    [SerializeField]
    private Image _foreground;
    
    private int maxValue= 100;
    [SerializeField]
    private float currentValue = 0;
    [SerializeField]
    private int _speed;
    public bool upingSoulShkala;
    int current;
    [SerializeField]
    private float speed2;
    [SerializeField]
    private MobileInput _mobileInput;
    private float q;

    public float GetCurrentVal()
    {
        return currentValue;
    }

    public void SetCurrentVal(float c)
    {
        currentValue = c;
    }

    void Start()
    {

        SetCurrentVal(Gold_manager.stamina);
        Gold_manager.stamina = 0;
    }

    public void UpShkala()
    {

             q = (float)currentValue / maxValue;
            var t = Mathf.Lerp(_foreground.fillAmount, q, _speed * Time.deltaTime);
            _foreground.fillAmount = t;

        if (currentValue > 100)
        {
          
            currentValue = 100;
        }
        else if (currentValue <= 0)
        {
            StartCoroutine(_mobileInput.ghostEffectEnd());
            upingSoulShkala = false;
            currentValue = 0;
        }




        if (upingSoulShkala)
        {

             currentValue = Mathf.MoveTowards(currentValue, 0,speed2* Time.deltaTime);
            
        }
        
    }

    public void setUpingSkalaBool(float current)
    {
  
        currentValue += current;

    }
    public IEnumerator setUpingSkalaBool2(float current) 
    {
        yield return new WaitForSeconds(0.2f);
        setUpingSkalaBool(current);
    }





    public void setUping()
    {
        upingSoulShkala = true;
    }
    public void nonSetUping()
    {
        upingSoulShkala = false;
    }
}
