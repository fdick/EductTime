using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnap : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private RectTransform[] _levels;
    public RectTransform rectTransfrom;
    private RectTransform myRect;
   [SerializeField]
    private int blockID;
    private int blockIDLeft;
    private int blockIDRight;
    private Vector2 vect2;
    [SerializeField]
    private bool isScrolling;
    float bttnDistance;
    [SerializeField]
    private RectTransform leftRect;
    [SerializeField]
    private RectTransform rightRect;
    [SerializeField]
    private ScrollRect scrollRect;

    public  bool leftBool;
    public bool rightBool;

    private float[] deltaDisRight;
    private float[] deltaDisLeft;

    void Awake()
    {
        bttnDistance = Mathf.Abs( _levels[1].anchoredPosition.x - _levels[2].anchoredPosition.x);
        _levels = new RectTransform[6];
        deltaDisLeft = new float[6];
        deltaDisRight = new float[6];

       myRect = GetComponent<RectTransform>();

        _levels[0] = transform.Find("Lvl0 Canvas").GetComponent<RectTransform>();
        _levels[1] = transform.Find("Lvl1 Canvas").GetComponent<RectTransform>();
        _levels[2] = transform.Find("Lvl2 Canvas").GetComponent<RectTransform>();
        _levels[3] = transform.Find("Lvl3 Canvas").GetComponent<RectTransform>();
        _levels[4] = transform.Find("Lvl4 Canvas").GetComponent<RectTransform>();
        _levels[5] = transform.Find("Lvl5 Canvas").GetComponent<RectTransform>();
        vect2 = new Vector2(900, myRect.anchoredPosition.y); // установить вид на первый элемент (то есть смотреть на первый лвл)
      
    }


    void Update()
    {
      
        // LEFT
        for (int i = 0; i < deltaDisLeft.Length; i++)
        {
            deltaDisLeft[i] = Mathf.Abs(leftRect.position.x - _levels[i].position.x);
        }
        float minDistanceLeft = Mathf.Min(deltaDisLeft);

        for (int i = 0; i < deltaDisLeft.Length; i++)
        {
            if (minDistanceLeft == deltaDisLeft[i])
            {
                blockIDLeft = i;
            }
        }

        //end



        //RIGHT
        for (int i = 0; i < deltaDisRight.Length; i++)
        {
            deltaDisRight[i] = Mathf.Abs(rightRect.position.x - _levels[i].position.x);
        }
        float minDistanceRight = Mathf.Min(deltaDisRight);

        for (int i = 0; i < deltaDisRight.Length; i++)
        {
            if (minDistanceRight == deltaDisRight[i])
            {
                blockIDRight = i;
            }
        }

        //end




        if (!isScrolling)
        {
            if (leftBool)
            {
                float newX = Mathf.Lerp(myRect.anchoredPosition.x, (blockIDLeft - 1) * -bttnDistance, speed * Time.deltaTime);
                vect2 = new Vector2(newX, myRect.anchoredPosition.y);
                if (myRect.anchoredPosition.x < (blockIDRight - 1) * -bttnDistance + 10 && myRect.anchoredPosition.x > (blockIDRight - 1) * -bttnDistance + -10)
                {
                  leftBool = false;
                }
            }
            if (rightBool)
            {
                float newX = Mathf.Lerp(myRect.anchoredPosition.x, (blockIDRight - 1) * -bttnDistance, speed * Time.deltaTime);
                vect2 = new Vector2(newX, myRect.anchoredPosition.y);
                if (myRect.anchoredPosition.x < (blockIDRight - 1) * -bttnDistance + 10 && myRect.anchoredPosition.x > (blockIDRight - 1) * -bttnDistance + -10)
                {
                  rightBool = false;
                }
            }

            myRect.anchoredPosition = vect2;
        }
    }

    public void scroll(bool s)
    {
        isScrolling = s;
    }

    //____________________________________________________________________

     public   IEnumerator setBOOL()
     {

        yield return new WaitForSeconds(0.1f);
        if (scrollRect.velocity.x > 0)
        {

            leftBool = true;
        }
        if (scrollRect.velocity.x < 0)
        {

            rightBool = true;
        }
    }

    //____________________________________________________________________

    public void setBool()
    {
        StartCoroutine(setBOOL());
     }
}
