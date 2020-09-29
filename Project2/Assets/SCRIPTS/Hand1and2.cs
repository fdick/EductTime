using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand1and2 : MonoBehaviour
{
    [SerializeField] private GameObject _positionBoss;
    [SerializeField] private GameObject _bossDiablo;
    private Animator _animBoss;
    [SerializeField] private bool _hand1;

    void Start()
    {
      
        _animBoss = _bossDiablo.GetComponent<Animator>();
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
           
            _bossDiablo.transform.position = _positionBoss.transform.position;
            if (_hand1)
            {
                _animBoss.Play("Hand1(jump)");
               
            }
            else
            {
                _animBoss.Play("Hand2(Tackle)");
            }
            
      
        }
    }
}
