using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSCR : MonoBehaviour
{
    private Transform takeParticle;
    private Transform soul_particle;

    private void Awake()
    {
        soul_particle = transform.GetChild(0).GetChild(0);
        takeParticle = transform.GetChild(1);
      

        
    }
 
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            
            takeParticle.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            soul_particle.localScale = soul_particle.localScale / 1.1f;
            if (soul_particle.localScale.x <= 0.5f)
            {
                soul_particle.parent.gameObject.SetActive(false);
            }
        }
    }
}
