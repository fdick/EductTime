using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTable : MonoBehaviour, IDragHandler, IEndDragHandler
{

    [SerializeField] Vector2 pointerDelta;
    RectTransform rect;
    public float speed = 5;
    public float topBlock;
    public float botBlock;
    public float leftBlock;
    public float rightBlock;

    [SerializeField] RectTransform topPanel;
    [SerializeField] RectTransform leftPanel;

    int defaultHeight = 2560;
   [SerializeField] int nativeHighScreen = 2960; // для разных экранов разное значение
    [SerializeField] int constanta = 0; // для разных экранов разное значение


    float deletel = 0;
    #region Inertsia
    public float deltaX;
    public float deltaY;
    public bool endHold = false;
    private float inertsia;
    public float inertsiaPublic;
    public bool movingPointer;
    #endregion
    private void Start()
    {
        deletel = nativeHighScreen / Screen.height;
        rect = GetComponent<RectTransform>();
        setStartPositionTable((int)TimeTable.instance.dt.DayOfWeek);
    }
    public void OnDrag(PointerEventData eventData)
    {
        endHold = false;
        inertsia = inertsiaPublic;
        deltaX = eventData.delta.x;
        deltaY = eventData.delta.y;


        //print(eventData.IsPointerMoving());
        movingPointer = eventData.IsPointerMoving();
        pointerDelta = eventData.delta;
        transform.position = new Vector2(Mathf.Lerp(transform.position.x, transform.position.x + pointerDelta.x, speed), Mathf.Lerp(transform.position.y, transform.position.y + pointerDelta.y, speed));

        topPanel.transform.position = new Vector2(transform.position.x, topPanel.transform.position.y);
        leftPanel.transform.position = new Vector2(leftPanel.transform.position.x, transform.position.y - constanta /deletel);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endHold = true;
        pointerDelta = Vector2.zero;

    }

    private void Update()
    {
        if (endHold && inertsia > 0 && movingPointer && (deltaX != 0 && deltaY != 0))
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, transform.position.x + (deltaX * inertsia), speed * Time.deltaTime), Mathf.Lerp(transform.position.y, transform.position.y + (deltaY * inertsia), speed * Time.deltaTime));

            topPanel.transform.position = new Vector2(transform.position.x, topPanel.transform.position.y);
            leftPanel.transform.position = new Vector2(leftPanel.transform.position.x, transform.position.y - constanta/deletel);
            inertsia = Mathf.Lerp(inertsia, 0, speed * Time.deltaTime);
            if (inertsia <= 0)
            {
                movingPointer = false;
            }
        }


        if (rect.localPosition.x <= rightBlock)
        {
            rect.localPosition = new Vector2(rightBlock, rect.localPosition.y);
            topPanel.transform.localPosition = new Vector2(rightBlock, topPanel.transform.localPosition.y);
        }
        if (rect.localPosition.x >= leftBlock)
        {
            rect.localPosition = new Vector2(leftBlock, rect.localPosition.y);

            topPanel.transform.localPosition = new Vector2(leftBlock, topPanel.transform.localPosition.y);
        }

        if (rect.anchoredPosition.y >= botBlock)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, botBlock );
            leftPanel.transform.localPosition = new Vector2(leftPanel.transform.localPosition.x, botBlock -constanta/1.6f);
        }
        if (rect.anchoredPosition.y <= topBlock)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, topBlock );

            leftPanel.transform.localPosition = new Vector2(leftPanel.transform.localPosition.x,topBlock - constanta/1.6f);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print(eventData.delta.x);
    }

   public void setStartPositionTable(int a)
    {
        int posX = 0;
        int posY = -627;
        switch (a)
        {
            case 1:
                posX = 447;
                break;
            case 2:
                posX = 172;
                break;
            case 3:
                posX = -166;
                break;
            case 4:
                posX = -450;
                break;
            case 5:
                posX = -812;
                break;
            case 6:
                posX = -812;
                break;
            default:
                break;
        }
        gameObject.transform.localPosition = new Vector2(posX, posY);
        leftPanel.transform.position = new Vector2(leftPanel.transform.position.x, transform.position.y - constanta / deletel);
        topPanel.transform.position = new Vector2(transform.position.x, topPanel.transform.position.y);
        
    }
}
