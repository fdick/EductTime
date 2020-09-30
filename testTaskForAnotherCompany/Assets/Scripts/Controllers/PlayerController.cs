using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<Actions> L_actionsUses = new List<Actions>();
    private List<GameObject> PoolOfBullets = new List<GameObject>();
    [SerializeField] private GameObject prefabOfBullet;
    [SerializeField] private int SizeOfPool = 10;
    [SerializeField] private Transform OriginForbullet;

    private float originTime;



    private void Awake()
    {
        originTime = Time.time;
        FillThePool();
    }
    private void FixedUpdate()
    {
            for (int i = 0; i < L_actionsUses.Count; i++)
            {
                try
			    {
				    L_actionsUses[i].Move(gameObject.transform);
				    L_actionsUses[i].Rotate(gameObject.transform);
				    L_actionsUses[i].Attack(ref originTime, ChooseReadyBullet(), gameObject.transform,OriginForbullet);
			    }
			    catch (System.Exception)
			    {
				    continue;

			    }
            }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy" || other.tag == "Wall")
        {
            transform.position = new Vector3(0, 18,-10);
        }
       

    }



    private void FillThePool()
    {
        for (int i = 0; i < SizeOfPool; i++)
        {
            GameObject go = Instantiate(prefabOfBullet, Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            PoolOfBullets.Add(go);
        }
    }

    private GameObject ChooseReadyBullet()
    {
        for (int i = 0; i < PoolOfBullets.Count; i++)
        {
            if (!PoolOfBullets[i].activeSelf)
            { 
                return PoolOfBullets[i];
            }
        }
        return null;
    }
}
