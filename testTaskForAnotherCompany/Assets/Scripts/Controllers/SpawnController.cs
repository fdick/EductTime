using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> L_bigCubes = new List<GameObject>();
    [SerializeField] private List<GameObject> L_smallCubes = new List<GameObject>();
    [SerializeField] private GameObject bigCubePrefab;
    [SerializeField] private GameObject smallCubePrefab;

    [SerializeField] private GameObject mapSizeE;
    [SerializeField] private GameObject mapSizeS;

    [SerializeField] private GameObject spawnSizeE;
    [SerializeField] private GameObject spawnSizeS;

    private static SpawnController Instance;
    public static SpawnController _instance => Instance;
    private void Awake()
    {
        Instance = this;
        FillThePool();
    }

    private void Update()
    {
        SpawnBigEnemy();
    }

    void SpawnBigEnemy()
    {
        int rand = Random.Range(0, 2);
        float x = 0;
        float z = 0;
        switch (rand)
        {
            case 0:
                x = Random.Range(spawnSizeS.transform.position.x, mapSizeS.transform.position.x);
                z = Random.Range(spawnSizeS.transform.position.z, spawnSizeE.transform.position.z);
                break;
            case 1:
                x = Random.Range(mapSizeE.transform.position.x, spawnSizeE.transform.position.x);
                z = Random.Range(spawnSizeS.transform.position.z, spawnSizeE.transform.position.z);
                break;
        }
        
        float angle = Random.Range(-90, 90);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);

        if (CanISpawnEnemy())
        {
            for (int i = 0; i < L_bigCubes.Count; i++)
            {
                if (!L_bigCubes[i].activeSelf)
                {
                    GameObject go = L_bigCubes[i];
                    go.SetActive(true);
                    go.transform.position = new Vector3(x, mapSizeE.transform.position.y, z);
                    go.transform.rotation = q;
                    return;
                }
            }

        }
    }

    public void SpawnSmallEnemies(Transform bigEnemy)
    {
        Quaternion q;
        int count = 0;
        for (int i = 0; i < L_smallCubes.Count; i++)
        {
            
            if (!L_smallCubes[i].activeSelf && count < 2)
            {
                GameObject go = L_smallCubes[i];
                go.SetActive(true);
                float angle = Random.Range(-90, 90);
                q = Quaternion.AngleAxis(angle, Vector3.up);
                go.transform.rotation = q;
                go.transform.position = new Vector3(bigEnemy.position.x + i/ 5, bigEnemy.position.y, bigEnemy.position.z + i/4);
                count++;
            }
          
        }
    }
    bool CanISpawnEnemy()
    {
        for (int i = 0; i < L_bigCubes.Count; i++)
        {
            if (!L_bigCubes[i].activeSelf)
            {
                int q = 0;
                for (int j = 0; j < L_smallCubes.Count; j++)
                {
                    if (!L_smallCubes[j].activeSelf)
                    {
                        q++;
                        if (q > 1)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    void FillThePool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(bigCubePrefab, transform);
            go.SetActive(false);
            L_bigCubes.Add(go);

            for (int j = 0; j < 2; j++)
            {
                GameObject go2 = Instantiate(smallCubePrefab, transform);
                go2.SetActive(false);
                L_smallCubes.Add(go2);
            }
        }
    }
}
