using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LvlUpCloser : MonoBehaviour, IPointerDownHandler
{
   
    public void OnPointerDown(PointerEventData data)
    {
        gameObject.SetActive(false);
        
    }
}
