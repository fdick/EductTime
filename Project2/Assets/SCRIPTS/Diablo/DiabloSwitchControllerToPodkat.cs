using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DiabloSwitchControllerToPodkat : MonoBehaviour
{
    [SerializeField]
     public CharacterController _charControllerForSwitch;
    private DiabloController _diabloControolerSCR;
    private DiabloAnimation _diabloAnimation;
    private Vector3 _moveDirection;
     [SerializeField]
     private  int _gravity;
    [SerializeField]
    private float _speed;
    [HideInInspector]
    public bool alone;

    public event Action goNextBlockPodkat;


    void Awake()
    {
        alone = true;
        _diabloAnimation = GetComponent<DiabloAnimation>();
        _charControllerForSwitch = GetComponent<CharacterController>();
        _charControllerForSwitch.enabled = false;
        _moveDirection = Vector3.zero;
        _diabloControolerSCR = gameObject.transform.parent.GetComponent<DiabloController>();
    }

    void Update()
    {
        if (_diabloControolerSCR.canPodkat && _charControllerForSwitch.enabled && !_charControllerForSwitch.isGrounded)   // Если я не на земле  во время подката, то включить анимацию падения
        {
          
            _diabloAnimation._animator.SetBool("jump", true);

        }
        else if (_charControllerForSwitch.enabled && _charControllerForSwitch.isGrounded)
        {
            _diabloAnimation._animator.SetBool("jump", false);
        }


        //if (_diabloControolerSCR.revivalPanel.revive)
        //{
        //    _diabloControolerSCR.music.mute = false;
        //    StartCoroutine(_diabloControolerSCR.UnmuteMusic());
        //}
    }
   
    void FixedUpdate()
    {
        _speed = _diabloControolerSCR._speed;

        if (_charControllerForSwitch.isGrounded)
        {
            _diabloControolerSCR.localIsGrounded = true;
        }

       
        Gravity();
    }


    public  void SwitchControllerToPodkat()  //Переключение коллайдера в режим подката
    {
        
        _charControllerForSwitch.enabled = !_charControllerForSwitch.enabled;
    }

    private void Gravity()
    {
        if (_charControllerForSwitch.enabled)
        {
            if (!_charControllerForSwitch.isGrounded)
            {
                _moveDirection.y -= _gravity * Time.deltaTime;
               

                _diabloControolerSCR.canPodkat = false;
            }
            else 
            {
                _moveDirection.y -= 1f * Time.deltaTime;
                _moveDirection.z = _speed;
               
              

            }

            _charControllerForSwitch.Move(_moveDirection);
        }
  
    }

    //_________________________________________________________________________________________________________________________

    void OnTriggerEnter(Collider col)  // Пересечение коллайдера блока Another Reality
    {

        if (col.tag == "ChangeTheReality")
        {
            var g = col.GetComponent<ChangeWorldTrigger>();
            _diabloControolerSCR._g = g;
        }

        if (col.tag == "TriggerBlockPosition")
        {
            var q = col.GetComponent<TriggerForCreateBlock>();
            _diabloControolerSCR.numberBloc = q.numberBlock;  // Получение константы нового блока
            _diabloControolerSCR.blocPosition = q.blockPosition; // Получение позиции нового блока
            _diabloControolerSCR.newBlock = true;
            if (_diabloControolerSCR.blocPosition != null)
            {
                _diabloControolerSCR.generic.GenerateBlock();
            }


        }
        if (col.tag == "GoNextBlock")
        {
           if (!_diabloControolerSCR.AlreadyInCollider)
           {
 
                _diabloControolerSCR.AlreadyInCollider = true;
                goNextBlockPodkat();
            }
            
            
        }

        if (col.tag == "Death_OnGround")
        {
            StartCoroutine(_diabloControolerSCR.muteMusic());
            Xp_manager.timesToShowAds++;
            _diabloControolerSCR.lose_sound.Play();
            _diabloControolerSCR._speed = 0;
            _diabloControolerSCR._defaultSpeed = 0;
            _diabloControolerSCR._forceSpeed = 0;
            _diabloControolerSCR.DeathFade();
             _diabloAnimation._animator.SetTrigger("Death2");
            alone = false;
            _diabloControolerSCR.StartEvent1(); // Запустить ивент
           


        }
        if (col.tag == "Death_Out")
        {
            StartCoroutine(_diabloControolerSCR.muteMusic());
            Xp_manager.timesToShowAds++;
            _diabloControolerSCR.DeathFade();
            if (_diabloControolerSCR.live)
            {
                _diabloControolerSCR.lose_sound.Play();
                _diabloControolerSCR.StartEvent1(); // если ударился, то запустить ивент
            }
            StartCoroutine(_diabloControolerSCR.OffScript());
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _diabloControolerSCR._metronom.Play("Metronom_endScale");
            }


        }


    }

    void OnTriggerExit(Collider col)
    {

        if (col.tag == "ChangeTheReality")
        {
            _diabloControolerSCR._g = null;

        }

        if (col.tag == "TriggerBlockPosition")
        {
            _diabloControolerSCR.newBlock = false;
        }
        if (col.tag == "GoNextBlock")
        {
                _diabloControolerSCR.AlreadyInCollider = false;
        }
    }

    //_________________________________________________________________________________________________________________________
}
