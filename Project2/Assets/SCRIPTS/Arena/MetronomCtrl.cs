using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetronomCtrl : MonoBehaviour
{
    public Timer timer;
    public DiabloController _diabloCtrl;
    private Text metrsText;
    [SerializeField]
    private float metros;
    [SerializeField]
    private Animator _animator;
    void Start()
    {
        metrsText = transform.Find("Metr's").GetComponent<Text>();
        _animator = GetComponent<Animator>();
    }

    public int GetMetros()
    {
        return (int)metros;
    }
    void Update()
    {
        if (!timer.loseORwin)
        {
            metros += Time.deltaTime * (_diabloCtrl._speed + 2);
            metrsText.text = ((int)metros).ToString();

            if ((int)metros < 500)
            {
                if ((int)metros%50 == 0)
                {
                
                    _animator.Play("Metronom_over1000");
                }
            }
            else if ((int)metros > 500 && (int)metros < 3000)
            {
                if ((int)metros % 500 == 0)
                {
                    _animator.Play("Metronom_over1000");
                }
            }
            else if ((int)metros > 3000 && (int)metros < 20000)
            {
                if ((int)metros % 1000 == 0)
                {
                    _animator.Play("Metronom_over1000");
                }
            }
            else if ((int)metros > 20000 && (int)metros < 1000000)
            {
                if ((int)metros % 10000 == 0)
                {
                    _animator.Play("Metronom_over1000");
                }
            }
        }
       
    }
}
