using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closer_Stena : MonoBehaviour
{
    [SerializeField]
    private int _active_pos;
    [SerializeField]
    private float speed;
    bool yetUse = false;

    private void MoveStena()
    {
     
        float q = Mathf.MoveTowards(transform.position.y, _active_pos, speed);

        gameObject.transform.position = new Vector3(transform.position.x, q, transform.position.z);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !yetUse)
        {
            yetUse = true;
            StartCoroutine(EveryOneSecond());
        }
    }

    public IEnumerator EveryOneSecond() // Замена fixedUpdate'у каждые 0.4 с запускается корутина и тем самым отпускает стену 
    {

        MoveStena();
        yield return new WaitForSeconds(0.1f);
        if (transform.localPosition.y > 4)
        {
            StartCoroutine(EveryOneSecond());

        }
       
    }

}
