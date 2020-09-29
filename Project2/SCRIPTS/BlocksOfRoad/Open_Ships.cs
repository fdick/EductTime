using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Ships : MonoBehaviour
{
    [SerializeField]
    private Transform _go_up;
    [SerializeField]
    private Transform _go_down;
    [SerializeField]
    private float up;
    [SerializeField]
    private float deffault;
    [SerializeField]
    private float down;

    [SerializeField]
    private bool upAndDown = false;  // Если истина, то выпускает и верхние и нижние шипы, иначе рандом

    bool yetUse = false;

   

   void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (!yetUse)
            {

                UpOrDown();
                yetUse = true;
            }
        }
    }

    void OnDisable()
    {
        yetUse = false;
        _go_down.transform.localPosition = new Vector3(0, deffault, 0);
        _go_up.transform.localPosition = new Vector3(0, deffault, 0);
        print("DisableShips");
    }

    void UpOrDown()
    {
        int q;
        if (!upAndDown)
        {
             q = Random.Range(0, 2);
        }
        else
        {
            q = 2;
        }
       
        if (q == 0)
        {
            _go_up.transform.localPosition = new Vector3(0, up, 0);
        }
        else if (q ==1)

        {
            _go_down.transform.localPosition = new Vector3(0, down, 0);
        }
        else if (q==2)
        {
            _go_up.transform.localPosition = new Vector3(0, up, 0);
            _go_down.transform.localPosition = new Vector3(0, down, 0);
        }
    }
}
