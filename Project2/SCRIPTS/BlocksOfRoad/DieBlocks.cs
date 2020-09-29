using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBlocks : MonoBehaviour
{
   

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("You die");
        }
    }
}
