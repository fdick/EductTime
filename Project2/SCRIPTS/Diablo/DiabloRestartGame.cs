using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiabloRestartGame : MonoBehaviour
{

    [SerializeField]
    private DiabloController _diabloController;
    
    public void CheckPoint()
    {
       _diabloController.live = !_diabloController.live;
        gameObject.transform.position = _diabloController.CheckPoint.transform.position;
        StartCoroutine(_diabloController.liveOn());
    }

   public void Restart(int i)
    {
        Application.LoadLevel(i);
        Time.timeScale = 1;
 
    }

  
}
