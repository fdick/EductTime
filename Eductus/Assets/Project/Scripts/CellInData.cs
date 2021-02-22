using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellInData : MonoBehaviour, IPointerDownHandler
{
    public Text[] texts;
    public Dropdown dropDown;
    public ObjectData objectData;

    public void OnPointerDown(PointerEventData eventData)
    {
        objectData = null;
        gameObject.SetActive(false);
       
    }



}
