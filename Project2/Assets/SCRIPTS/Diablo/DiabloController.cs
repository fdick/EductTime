using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;


public class DiabloController : MonoBehaviour
{
    public GenerationWORLD generic;
    [SerializeField]
    private DataDiablo _dataDiablo;
    [HideInInspector]
    public CharacterController _charController;
    private DiabloAnimation _diabloAnimationSCR;
    private DiabloSwitchControllerToPodkat _diabloSwitchControllerSCR;
    public CameraController camCotroller;
    public MobileInput mobileInput;
    [HideInInspector]
    public Transform GODiablo;
    [SerializeField]
    private ShkalaSouls _shkalaSouls;



    [SerializeField]
    private float _gravity = 0;
    [HideInInspector]
    public Vector3 _moveDirection = Vector3.zero;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpForceCeiling;

    [SerializeField]
    private float jumpForceDefault;
    public bool canJump;
    //[HideInInspector]
    public bool canPodkat;
    [SerializeField]
    public float _defaultSpeed;
    [SerializeField]
    public float _forceSpeed;
    [SerializeField]
    public float _speed;
    [SerializeField]
    private float defaultAnimationSpeed;
    [HideInInspector]
    public ChangeWorldTrigger _g;
    public bool win;


    public bool localIsGrounded;
    public bool isCeiling;

    public bool canRun_;
    public bool newBlock;
    public bool Ijumping;
    public bool live;
    public bool AlreadyInCollider = false;

    //[HideInInspector]
    public int numberBloc;
    public Transform blocPosition;
    [SerializeField]
    public GameObject CheckPoint;
    [SerializeField]
    private GameObject _deathFade;
    [SerializeField]
    public Animator _metronom;

    public static event Action deathEvent; // Ивент для тригера на стене лвл1, последний блок


    public event Action goNextBlock;


    public AudioSource lose_sound;
    public AudioSource music;
    public RevivePanel revivalPanel;

   // [SerializeField] private GlobalFunctions globalF;





 
 


    void Awake()
    {
       // globalF = FindObjectOfType<GlobalFunctions>().GetComponent<GlobalFunctions>();
       
       
        live = true;
        defaultAnimationSpeed = 1f; // Установление дефолтной скорости анимации бега


        Ijumping = false;
        _speed = _defaultSpeed;
        jumpForce = 0.4f;
        GODiablo = gameObject.GetComponent<Transform>();
        newBlock = false;
        canJump = true;
        canPodkat = true;
        isCeiling = false;
        _charController = GetComponent<CharacterController>();
        _diabloAnimationSCR = gameObject.transform.GetChild(0).GetComponent<DiabloAnimation>();
        _diabloSwitchControllerSCR = gameObject.transform.GetChild(0).GetComponent<DiabloSwitchControllerToPodkat>();
        canRun_ = true;
      




    }

    void Update()
    {
        
        if (_charController.isGrounded )
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (_charController.isGrounded && canJump)
        {
            canRun_ = true;
        }
        else canRun_ = false;
        if (!canPodkat)
        {
            canRun_ = false;
        }

        if (revivalPanel != null)
        {


            if (revivalPanel.revive)
            {
               
                music.mute = false;
                StartCoroutine(UnmuteMusic());

            }
        }
      

    }

    
    void FixedUpdate()
    {
        if (live) // Если я жив, то юзать
        {


            Run();
          
            InputMobile();
            Gravity();
            Animations();

            
        }

    }

  

    private void Gravity()  // Физика гравитации
    {
        if (_charController.enabled)
        {
            if (!_charController.isGrounded)
            {
                _moveDirection.y -= _gravity * Time.deltaTime;
                
            }
            _charController.Move(_moveDirection);       
        }
    }

   

    private void InputMobile()  //Управление телефоном
    {
        if (mobileInput.swipe == MobileInput._swipes.up)
        {
            
            if (_charController.isGrounded && canJump)
            {
               canJump = false;
                Ijumping = true;
                Jump();   // Прыжок
                mobileInput.swipe = MobileInput._swipes.nothing;
            }

            if (mobileInput.localEndEffect && mobileInput.iBeginDrag) // Если мы начали держать тач и свайпать, то вопроизвести окончание эффекта
            { // Начать уменьшение шкалы
                if (_g != null)
                {
                    _g.ChangeBlocks(true);// Если во время тача, мы начинаем свайпать, то переключить снова блоки
                }
                mobileInput._shkala.nonSetUping();
                StartCoroutine(mobileInput.ghostEffectEnd());
                mobileInput.localEndEffect = false;
                mobileInput.iBeginDrag = false;

            }
            
        }

        else if (mobileInput.swipe == MobileInput._swipes.down)
        {
            if (_charController.isGrounded && canPodkat)
            {
                
                canPodkat = false;
                canJump = false;
                SwitchCollider();
                mobileInput.swipe = MobileInput._swipes.nothing;
                _diabloAnimationSCR.podkatfunc();  // Функция подката
                mobileInput._shkala.nonSetUping();

            }

            if (mobileInput.localEndEffect && mobileInput.iBeginDrag)  // Если мы начали держать тач и свайпать, то вопроизвести окончание эффекта
            {
                if (_g != null)
                {
                    _g.ChangeBlocks(true);  // Если во время тача, мы начинаем свайпать, то переключить снова блоки
                }
               
                mobileInput._shkala.nonSetUping();
                StartCoroutine(mobileInput.ghostEffectEnd());
                mobileInput.localEndEffect = false;
                mobileInput.iBeginDrag = false;
            }

        }

        Move(); // Передвижение 
    }

    void OnTriggerEnter(Collider col)  // Пересечение коллайдера блока Another Reality
    {
        
        if (col.tag == "ChangeTheReality")
        {
            var g = col.GetComponent<ChangeWorldTrigger>();
            _g = g;
        }

        if (col.tag == "TriggerBlockPosition")
        {
            var q = col.GetComponent<TriggerForCreateBlock>();
            numberBloc = q.numberBlock;  // Получение константы нового блока
            blocPosition = q.blockPosition; // Получение позиции нового блока
            newBlock = true;
            if (blocPosition != null)
            {
                generic.GenerateBlock();
            }
          

        }
        if (col.tag == "GoNextBlock" && goNextBlock != null)
        {
            if (!AlreadyInCollider)
            {
             
                AlreadyInCollider = true;
                goNextBlock();
           }
            
        }

        if (col.tag == "Soul")
        {

            _shkalaSouls.setUpingSkalaBool(8); //Поднятие шкалы на 4 единицы
        }
        if (col.tag == "Death_OnGround")
        {
            StartCoroutine(muteMusic());


            Xp_manager.timesToShowAds++;
            DeathFade();
            _diabloAnimationSCR._animator.SetTrigger("Death1");
            _defaultSpeed = 0;
            _speed = 0;
            _forceSpeed = 0;
            if (live)
            {
                lose_sound.Play();
                StartEvent1(); // если ударился, то запустить ивент
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _metronom.Play("Metronom_endScale");
            }

            if (Xp_manager.timesToShowAds >= 10)
            {
                ShowInterstitial();
                Xp_manager.timesToShowAds = 0;
            }

        }
        if (col.tag == "Death_Out")
        {
            StartCoroutine(muteMusic());



            Xp_manager.timesToShowAds++;
            DeathFade();
            if (live)
            {
                lose_sound.Play();
                StartEvent1(); // если ударился, то запустить ивент
            }
            StartCoroutine(OffScript());
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _metronom.Play("Metronom_endScale");
            }
           

        }

        if (col.tag == "CaveNOjump")
        {
            jumpForce = jumpForceCeiling;  // Если мы зашли в зону пещер, то уменьшить силу прыжка
        }


        if (Xp_manager.timesToShowAds >= 10)
        {
            ShowInterstitial();
            Xp_manager.timesToShowAds = 0;
        }
    }

    void OnTriggerExit(Collider col)
    {
        
        if (col.tag == "ChangeTheReality")
        {
            if (_g != null)
            {
                _g.ChangeBlocks(true); // Если мы выходим за пределы тригера действия, то переключить блоки в исходное положение
                _g = null;
            }
           
           
        }

        if (col.tag == "TriggerBlockPosition")
        {
            newBlock = false;
        }

        if (col.tag == "CaveNOjump")
        {
            jumpForce = jumpForceDefault; // Вышли с зоны пещер, то увеличить силу прыжка
        }
        if (col.tag == "GoNextBlock")
        {
            AlreadyInCollider = false;
        }
    }




    /*________________________________________________________________________________________________*/

    public IEnumerator UnmuteMusic()
    {

        revivalPanel.revive = false;
        music.volume = Mathf.MoveTowards(music.volume, 0.8f, 0.2f);
        yield return new WaitForSeconds(0.7f);
        if (music.volume < 0.8f)
        {
            StartCoroutine(UnmuteMusic());
        }
        else
        {
            music.volume =0.8f;
            
            
        }

    }
    public IEnumerator muteMusic()
    {

        music.volume = Mathf.MoveTowards(music.volume, 0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        if (music.volume > 0)
        {
            StartCoroutine(muteMusic());
        }
        else
        {
            music.mute = true;
        }
    }

        public IEnumerator liveOn()   // Включение live
        {
        yield return new WaitForSeconds(0.05f);
        live = !live;
        }

    public IEnumerator OffScript()  // Выключение скрипта этого через время, при падении с высоты
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<DiabloController>().enabled = false;
    }

 


    /*________________________________________________________________________________________________*/

    void ShowInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL));
             Appodeal.show(Appodeal.INTERSTITIAL);
    }

    private void Run()
    {
        if (mobileInput.nothing && _speed != _forceSpeed && canRun_)  // Если механика бега, то разогнаться
        { 
            _speed = Mathf.Lerp(_speed, _forceSpeed, Time.deltaTime  * 1.2f);
            if(_forceSpeed > 0.8f)
            _diabloAnimationSCR._animator.speed = Mathf.Lerp(_diabloAnimationSCR._animator.speed, 1.5f, Time.deltaTime);  // Ускорение анимации бега больше, если форс спид больше
            else
                _diabloAnimationSCR._animator.speed = Mathf.Lerp(_diabloAnimationSCR._animator.speed, 1.2f, Time.deltaTime);  // Ускорение анимации бега меньше, если форс спид меньше
        }
        else if(_speed != _defaultSpeed || !canRun_ )                   // Иначе скорость падает до дефолтной 
        {
            if  (Ijumping && _speed > _defaultSpeed + 0.02f /*|| !canPodkat && _speed > _defaultSpeed + 0.02f*/)  // Если я в прыжке и моя скорость > дефолтной скорости + чуть чуть, потомучто при изменении скорости не точные значения.
            {
                _speed = Mathf.Lerp(_speed, _defaultSpeed, Time.deltaTime * 0.2f);  // Если набрано ускорение, и после произошел прыжок, то замедлять скорость медленне чем обычно
             
            }
            else if (!canPodkat && _speed > _defaultSpeed + 0.02f)  
            {
                _speed = Mathf.Lerp(_speed, _defaultSpeed, Time.deltaTime * 2.2f); //Если я в подкате, то скорость замедляется быстрее чем обычно (использовать подкат как тормоз)
            }
            else
            {
                _speed = Mathf.Lerp(_speed, _defaultSpeed, Time.deltaTime); //Иначе замедлять скорость нормально
                
            }
                _diabloAnimationSCR._animator.speed = Mathf.Lerp(_diabloAnimationSCR._animator.speed, defaultAnimationSpeed, Time.deltaTime);  // Замедление анимации бега
            
        }

        if (_speed >= _defaultSpeed + 0.02f && canRun_ && (mobileInput.swipeLeft || mobileInput.swipeRight) ) // Если я повышаю скорость, то попполнять шкалу взависимости от скорости
        {
       
            StartCoroutine(_shkalaSouls.setUpingSkalaBool2(0.07f));
            
        }
       
    }

    public void StartEvent1()
    {
        live = false;
        deathEvent();
        
    }

    private void Jump()
    {
      
            _moveDirection.y = jumpForce;
        
       
    }

    private void Move()
    {
        _moveDirection.z = _speed; 
    }

    public void DeathFade()
    {
        _deathFade.SetActive(true);
    }

    private void Animations()  //Вызов анимаций
    {
        _diabloAnimationSCR.isGroundfunc();
        _diabloAnimationSCR.isNotGroundfunc();
    }  

    public void SwitchCollider() //Переключение CharacterController
    {
        _charController.enabled = !_charController.enabled;
        _diabloSwitchControllerSCR.SwitchControllerToPodkat();
    }

  

    
}
