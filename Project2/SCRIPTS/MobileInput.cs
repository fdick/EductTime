using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public CameraController camCotroller;
    public DiabloController diabloController;
    [SerializeField]
    public ShkalaSouls _shkala;
    [SerializeField]
    private ChangeWorldTrigger _g;
    public float deltaSwipe;
    [SerializeField]

    public enum _swipes
    {
        right,
        left,
        up,
        down,
        nothing
    }
    public _swipes swipe;

    [SerializeField]
    private float CD; //кул даун ухода в иную реальность
    [SerializeField]
    private float currentTime; // time.time + CD 



    public bool holding;
    public bool hasCollider;
    public bool isTouch;
    public bool localEndEffect;
    public bool iBeginDrag;

    public bool swipeLeft = false;
    public bool swipeRight = false;
    public bool nothing = false;
    public bool inState = false;
    [SerializeField] private AudioSource music;





    void Start()
    {
        localEndEffect = false;

        iBeginDrag = false;
        swipe = _swipes.nothing;
        camCotroller.ghostEffectInt = 3;  // DEfault event the camera effect

    }


    void Update()
    {
        _g = diabloController._g;
        if (_g != null)
        {
            hasCollider = true;
        }
        else
        {
            hasCollider = false;
        }

        if (!holding) // Если отжали тач то выключить эффект
        {
            StartCoroutine(ghostEffectEnd());
        }

        if (holding && inState && localEndEffect && isTouch)
        {
            music.pitch = 0.9f;
        }
        else
        {
            music.pitch = 1;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (_shkala.GetCurrentVal() <= 0)
        {
            OffEffectAfterEndShkala();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }


    public void OnPointerDown(PointerEventData eventData)  // Нажатие
    {
        isTouch = true; // Это касание? - да

        StartCoroutine(holdingTime());  // После 0.2 включаем holding
        
        if (Time.time >= currentTime + CD)
        {
            inState = true;
            
            StartCoroutine(abc());    // Включение AnotherBlock + включение эффекта после 0.21
        }
       
    }



    public void OnPointerUp(PointerEventData eventData)   //Отжатие
    {
        OffEffectAfterEndShkala();
    }

    public void OffEffectAfterEndShkala()
    {
        if (swipe == _swipes.nothing)
        {
            if (holding)
            {
                if (hasCollider) _g.ChangeBlocks(true);  // Возвращает все блоки на прежнее место
                _shkala.nonSetUping();
                StartCoroutine(ghostEffectEnd());  // Выключение эффекта
                if (inState)
                {
                    currentTime = Time.time;
                    CD = 1f;
                    inState = false;
                }

            }

        }
        holding = false;
        isTouch = false; // Это касание? - нет
        localEndEffect = false;

        nothing = false;
    }




    /* ___________________________________________________________________________________________ */


    public IEnumerator ghostEffectStart()  // Effect старт
    {
       
        camCotroller._animator.SetBool("Start", true);
        camCotroller.ghostEffectInt = 1;
        yield return new WaitForSeconds(0.2f);
        camCotroller.ghostEffectInt = 2;

    }


    public IEnumerator ghostEffectEnd()  // Effect окончание
    {
   
        camCotroller._animator.SetBool("Start", false);
        yield return new WaitForSeconds(0.35f);
    }

    public IEnumerator holdingTime()    // Включает holding
    {
      
        yield return new WaitForSeconds(0.2f);
        if (isTouch )
        {
            holding = true;
            localEndEffect = true;
          
        }
    }

    public IEnumerator abc(){   // Переключение блоков
        yield return new WaitForSeconds(0.21f);
        if (swipe == _swipes.nothing)  // If ( Перс просто бежит )
        {
            if (holding)  // If ( Удерживание тача + нахождение в колайдере )
            {
                if (hasCollider && _shkala.GetCurrentVal() > 0) _g.ChangeBlocks(false); // Включение AnotherBlock и Отключение ThisBlock
                _shkala.setUping(); // Начать уменьшение шкалы
                if (_shkala.GetCurrentVal() > 0) { // Если есть стамина, то включить эффект
                    StartCoroutine(ghostEffectStart());  // Включение эффекта
                }
            }
        }
    }

   

   
    public IEnumerator NothingSwipeLeft()  // Корутина механики включения bool nothing
    {
        if (swipeLeft)
        {

            yield return new WaitForSeconds(0.3f);
           
            if (swipeRight)
            {
                nothing = true;
            }
         
            
        }
    }

    public IEnumerator NothingSwipeRight()
    {
        if (swipeRight)
        {
            yield return new WaitForSeconds(0.3f);
          
            if (swipeLeft)
            {
                nothing = true;
            }
           
            
        }
    }

    /* ___________________________________________________________________________________________ */




    public void OnDrag(PointerEventData eventData)
    {


        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 1)
            {
                swipe = _swipes.right;
                swipeRight = true;
                swipeLeft = false;
             


            }
            else if (eventData.delta.x < -1)
            {


                swipe = _swipes.left;
                swipeLeft = true;
                swipeRight = false;
                


            }
            else nothing = false;    // Если палец на экране, но не движется, то...

            if (swipeRight || swipeLeft)  // Если свайп влево или вправо, то механика включения bool nothing
            {
                StartCoroutine(NothingSwipeLeft());
                StartCoroutine(NothingSwipeRight());
            }
           
                if (localEndEffect && iBeginDrag) // Если мы начали держать тач и свайпать, то вопроизвести окончание эффекта
                {
                if (_g != null)
                {
                    _g.ChangeBlocks(true);  // Если во время тача, мы начинаем свайпать, то переключить снова блоки
                   
                }
                _shkala.nonSetUping(); // Перестать затухание шкалы
                StartCoroutine(ghostEffectEnd());
                    localEndEffect = false;
                    iBeginDrag = false;

                }
            
        }  
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        iBeginDrag = true;
        holding = false;
        isTouch = false;


        if (Mathf.Abs(eventData.delta.y) > Mathf.Abs(eventData.delta.x)) // Прыжок
        {
            if (eventData.delta.y > deltaSwipe && swipe == _swipes.nothing )
            {
                if (diabloController.canJump)
                {
                    swipe = _swipes.up;
                   
                }
               
              
            }
            else  // Подкат
            {
                if (swipe == _swipes.nothing)
                {
                    if (diabloController.canPodkat)
                    {
         
                        swipe = _swipes.down;
                       
                    }
                }

            }
            
        }

    }

    public void OnEndDrag(PointerEventData eventData)  // Конец удерживания пальца на экране
    {
        
        camCotroller.ghostEffectInt = 3;
        swipe = _swipes.nothing;
        swipeLeft = false;
        swipeRight = false;
        nothing = false;
        iBeginDrag = false;

    }
}
