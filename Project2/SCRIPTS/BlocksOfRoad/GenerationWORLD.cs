using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationWORLD : MonoBehaviour
{
    [SerializeField]
    private DataBlockOfWorld dataBlocks;
    [SerializeField]
    private List<GameObject> createdBlocks;
    [SerializeField]
    private DiabloController _diabloController;
    [SerializeField]
    private bool testing;
   
    


    void Awake()
    {
        if (!testing)  // Проверять если это не билдовская версия, то генерировать
        {
            for (int i = 0; i < dataBlocks.blocks.Count; i++)
            {
                var bloc = Instantiate(dataBlocks.blocks[i], gameObject.transform);
                bloc.SetActive(false);

                createdBlocks.Add(bloc);
            }
            createdBlocks[0].SetActive(true);  // Включить первый блок (по дефолту)
        }


    }



    public void GenerateBlock()
    {
        if (_diabloController.newBlock)
        {
            createdBlocks[_diabloController.numberBloc].SetActive(true); // Включение следующего блока
            createdBlocks[_diabloController.numberBloc].transform.position = _diabloController.blocPosition.position; // Установка позиции нового блока
        }
    }
}
