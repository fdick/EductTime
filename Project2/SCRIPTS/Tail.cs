using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField] private GameObject _positionBoss;
    [SerializeField] private GameObject _bossDiablo;
    [SerializeField] private GameObject _block;
    private Animator _animBlock;
    private Animator _animBoss;

    bool yetUse = false;

    void Start()
    {

        _animBoss = _bossDiablo.GetComponent<Animator>();
        _animBlock = _block.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !yetUse)
        {
            yetUse = true;
            print("InCollider2");
            _bossDiablo.transform.position = _positionBoss.transform.position;
            _animBoss.Play("Tail");
            _animBlock.Play("Block_animation");
            print("InCollider");
        }
    }
 

 
}
