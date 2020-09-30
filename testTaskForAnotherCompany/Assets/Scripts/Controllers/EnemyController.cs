using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distanceRay;
    [SerializeField] private bool bigEnemy;
    private RaycastHit rayHit;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Соударение с границами карты
        Debug.DrawRay(transform.position, transform.forward * distanceRay, Color.red);  
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, distanceRay))
            if (rayHit.transform.tag == "Wall")
                gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (bigEnemy)
                SpawnController._instance.SpawnSmallEnemies(gameObject.transform);

            gameObject.SetActive(false);
        }
       

    }
}
