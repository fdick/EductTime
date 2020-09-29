using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCube : MonoBehaviour
{
    [SerializeField]
    private List<Transform> pos;
    public GenerationWORLD generic;
    public DiabloController diablo;
    int i = 0;

    void OnTriggerEnter(Collider col)  // Пересечение коллайдера блока Another Reality
    {
        if (col.tag == "TriggerBlockPosition")
        {
            var q = col.GetComponent<TriggerForCreateBlock>();
            diablo.numberBloc = q.numberBlock;  // Получение константы нового блока
            diablo.blocPosition = q.blockPosition; // Получение позиции нового блока
            diablo.newBlock = true;

            generic.GenerateBlock();



        }
    }

    void Start()
    {
        i = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (i <= pos.Count)
            {
                gameObject.transform.position = pos[i].position;
            }
            Debug.Log("space");
            
            i++;
        }
    }

    
}
