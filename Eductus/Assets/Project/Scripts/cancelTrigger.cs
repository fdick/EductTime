using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cancelTrigger : MonoBehaviour, IPointerClickHandler
{
    Animator animator;
    OpenMainMenu menu;

    private void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        menu = transform.parent.GetComponent<OpenMainMenu>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        animator.Play("close");
        gameObject.SetActive(false);
        menu.humburger.GetComponent<Animator>().Play("Out");
    }
}
