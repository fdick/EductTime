using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Exit1(GameObject go)  // Крестик(скрывает парент и активирует go)
    {
        transform.parent.gameObject.SetActive(false);
        go.SetActive(true);
    }
}
