using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBlocks : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            gameObject.SetActive(false);
      
        }
    }


}
