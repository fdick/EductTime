using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
   
    private CheckPointSystem q;

    void Start()
    {
         q = transform.parent.GetComponent<CheckPointSystem>();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            for (int i = 0; i < q.boolCheckPoints.Length; i++)
            {
                if (q.boolCheckPoints[i] != true)
                {
                    q.boolCheckPoints[i] = true;
                    break;
                }
            }
           
        }
    }
}
