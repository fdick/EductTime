using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    private TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        trail.enabled = true;
        StartCoroutine(DeactivateMe());
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);  
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            trail.enabled = false;
            gameObject.SetActive(false);
        }
    }
    IEnumerator DeactivateMe()
    {
        yield return new WaitForSeconds(lifeTime);
        trail.enabled = false;
        gameObject.SetActive(false);
    }
}
