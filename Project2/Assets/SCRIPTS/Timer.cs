using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text _minutes;
    [SerializeField]
    private Text _seconds;
    //[HideInInspector]
    public bool loseORwin;
    
    [HideInInspector]
    public float secondTime;
    [HideInInspector]
    public int minuteTime;

    void Start()
    {
       
        loseORwin = false;
        secondTime = 0;
        minuteTime = 0;
        
        

    }

    void Update()
    {
        if (!loseORwin)
        {
            secondTime += Time.deltaTime;
            _seconds.text = ((int)secondTime).ToString();
            if (secondTime >= 60)
            {
                secondTime = 0;

            }
            if (secondTime == 0)
            {
                minuteTime++;
                _minutes.text = minuteTime.ToString();
            }
        }
    }
}
