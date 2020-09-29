using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiabloCanvasUI : MonoBehaviour
{
    [SerializeField]
    private ShkalaSouls _shkala;
  

    
    void Update()
    {
        _shkala.UpShkala();  // Отвечает за шкалу.
    }

    
}
