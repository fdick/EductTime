using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _blocks;  // Префабы
    [SerializeField]
    private List<GameObject> _instantiateBlocks = new List<GameObject>(); // Уже созданные и выключенные блоки
    [SerializeField]
    private List<GameObject> _dynamicsBlocks = new List<GameObject>(); // Массив для контроля ушедших блоков, то есть выключение блоков далеко позади
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private bool is2000 = false;
    [SerializeField]
    private int procedure = 1; // Если 1 = нейтрально, если 2 = поставить блок в начало, если 3 = переместить гг в начало
    private LevelController lvlController;
    private bool b; // Фиксатор, для того чтобы procedure один раз включило 2 и больше его не перебивала
    [SerializeField]
    private int r;
    [SerializeField]
    private int rDynamic;
    [SerializeField]
    private MetronomCtrl _metronom;



    public int y, z;

    private int levels;
    [SerializeField]
    private bool specialR;
    
    
    
    


    void Awake()
    {

        
        b = true;
        is2000 = true;
        specialR = false;

        lvlController = GetComponent<LevelController>();
        player.GetComponent<DiabloController>().goNextBlock += GenerationBlocks;  // Subscribe
        player.transform.Find("Diablo").GetComponent<DiabloSwitchControllerToPodkat>().goNextBlockPodkat += GenerationBlocks; // Subscribe
      
       
        InstantiateBlocks();
        GenerationBlocks();

        
    }

    void Update()
    {
        if (lvlController.level == LevelController.eLevels.Medium)
        {
            lvlController.ChangeFOGcolor(lvlController.MediumColor);
        }
        if (lvlController.level == LevelController.eLevels.Hard)
        {
            lvlController.ChangeFOGcolor(lvlController.HardColor);
        }

    }


    void InstantiateBlocks() // Сгенерировать блоки и сделать их невидимыми
    {
        for (int i = 0; i < _blocks.Length; i++)
        {
            var b = Instantiate(_blocks[i]);
            b.transform.SetParent(transform);
             b.SetActive(false);
            _instantiateBlocks.Add(b);
            


        }
        
    }

    public void GenerationBlocks() //Если вошел в триггер то сгенерировать следующий блок
    {
        if (_metronom.GetMetros() > 222 && _metronom.GetMetros() <=800) //Установка медиум лвла после 300 метров
        {
            lvlController.level = LevelController.eLevels.Medium;
        }
        if (_metronom.GetMetros() > 800)                                    //Установка медиум лвла после 800 метров
        {
            lvlController.level = LevelController.eLevels.Hard;
        }


        if (lvlController.level == LevelController.eLevels.Easy)
        {
            y = Random.Range(-5, 4);
            z = Random.Range(5, 8);
        }
        else if (lvlController.level == LevelController.eLevels.Medium)
        {
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                y = Random.Range(-9, -5);
            }
            else
            {
                y = Random.Range(3, 4);
            }

   
            z = Random.Range(8, 10);
            player.GetComponent<DiabloController>()._defaultSpeed = 0.25f;
            player.GetComponent<DiabloController>()._forceSpeed = 0.35f;
          

        }
        else if (lvlController.level == LevelController.eLevels.Hard)
        {
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                y = Random.Range(-9, -5);
            }
            else
            {
                y = Random.Range(3, 4);
            }


            z = Random.Range(10, 12);
            player.GetComponent<DiabloController>()._defaultSpeed = 0.30f;
            player.GetComponent<DiabloController>()._forceSpeed = 0.40f;
        }

       


        if (specialR)
        {
            specialR = false;
        }



        if (player.transform.position.z > 3000 && is2000)
        {
            is2000 = false;
            procedure = 2;
        }

        if (rDynamic == 10 || rDynamic == 11)
        {
            specialR = true;
        }

      //_______________________________________________________________________


            if (procedure != 3)
        {
            RandomR(12);
        }

        if (!specialR) // Если R не специальное, то генерировать R
        {
            IfActiveThenRandomR();
        }

        //_______________________________________________________________________



        if (rDynamic == 10 || rDynamic == 11)
        {
    
            y = -5;
            z = 2;
            r = 3;
            if (_instantiateBlocks[3].activeSelf)
            {
                r = 2;
            }
            print("LAST block is 10 or 11");
            print(r);
            print("rDynamic: " + rDynamic);
        }
        else if (rDynamic == 8 || rDynamic == 9) // Если 8 и 9, то не так далко спавнить следующий, так как там разбега нет
        {
            z = Random.Range(4, 6);
        }

       
            rDynamic = r;
        
        
        if (r ==8 || r ==9)
        {
            y = -28;
            z = 3;
        }

        if (_dynamicsBlocks.Count > 0)
        {
     

            if (procedure != 3)
            {
                _instantiateBlocks[r].transform.position = _dynamicsBlocks[_dynamicsBlocks.Count - 1].transform.Find("EndBlock").position;
                generateForLevelMap(y, z);
                _instantiateBlocks[r].SetActive(true);
                _dynamicsBlocks.Add(_instantiateBlocks[r]);
            }


            if (procedure == 2) // Если гг убежал дальше 2000 метров то вернуть в начало
            {
     
                if (r == 11 || r == 10)
                {
                    RandomR(12);
                }
                procedure = 3;
                if (r % 2 == 0)
                {

                    _instantiateBlocks[r + 1].transform.position = new Vector3(0, 0, 50);
                    _instantiateBlocks[r + 1].SetActive(true);

                    _dynamicsBlocks.Add(_instantiateBlocks[r + 1]);

                }
                else
                {

                    _instantiateBlocks[r - 1].transform.position = new Vector3(0, 0, 50);
                    _instantiateBlocks[r - 1].SetActive(true);

                    _dynamicsBlocks.Add(_instantiateBlocks[r - 1]);
                }
                


            }
            else if (procedure == 3)
            {
                is2000 = true;
            
                BackToBegin();
                procedure = 1; // Сделать дефолтной
                b = true;
              
            }



            //__________Remove_______________________________________
            if (procedure != 2)
            {


                if (_dynamicsBlocks.Count == 4)
                {
                    _dynamicsBlocks[0].SetActive(false);
                    _dynamicsBlocks.RemoveAt(0);
                }
                else if (_dynamicsBlocks.Count > 4)
                {


                    _dynamicsBlocks[0].SetActive(false);
                    _dynamicsBlocks.RemoveAt(0);

                    _dynamicsBlocks[0].SetActive(false);
                    _dynamicsBlocks.RemoveAt(0);

                }
            }
            //_______________________________________________________


        }





        else if (_dynamicsBlocks.Count == 0)
        {
            r = Random.Range(0, 4);
            _instantiateBlocks[r].transform.localPosition = new Vector3(0.06f, -1.2f, 48.65f);
            _dynamicsBlocks.Add(_instantiateBlocks[r]);
            rDynamic = r;
        }

        for (int i = 0; i < _dynamicsBlocks.Count; i++)
        {
            _dynamicsBlocks[i].SetActive(true);
        }


    }

    private void IfActiveThenRandomR()
    {
        if (_instantiateBlocks[r].activeSelf)
        {
            for (int i = 0; i < 24; i++)
            {

                if (_instantiateBlocks[r].activeSelf)
                {
                    RandomR(12);

                }
                else
                {
                    if (r % 2 == 0 && _instantiateBlocks[r + 1].activeSelf)
                    {
                        RandomR(12);
                    }
                    else if (r % 2 != 0 && _instantiateBlocks[r - 1].activeSelf)
                    {
                        RandomR(12);
                    }
                    else
                    {
                        break;
                    }
                }
            }

        

        }
    }

    private void RandomR(int y)
    {


        if (lvlController.level == LevelController.eLevels.Medium)
        {
            y = 18;
        }
        else if (lvlController.level == LevelController.eLevels.Hard)
        {
            y = 24;
        }

        if (r % 2 == 0) // Если r четное, то ...
        {
          
            int a = r;
            int b = /*_instantiateBlocks.Count*/y - 1 - (r + 1);
            if (a > b)
            {
             
                r = Random.Range(0, r);
             
            }
            else
            {
              
                r = Random.Range(r + 2,y);
            
            }
        }
        else  // Если r нечетное, то ...
        {
           
            int a = r - 1;
            int b = y - 1 - r;
            if (a > b)
            {
              
                r = Random.Range(0, r - 1);
              
            }
            else
            {
             
                r = Random.Range((r + 1), y);
          
            }
        }
        
    }

    private void BackToBegin()
    {
        player.GetComponent<DiabloController>().AlreadyInCollider = false;
        player.transform.SetParent(_dynamicsBlocks[_dynamicsBlocks.Count - 2].transform);
        float z = player.transform.localPosition.z;
        float y = player.transform.localPosition.y;
        player.transform.SetParent(_dynamicsBlocks[_dynamicsBlocks.Count - 1].transform);
        player.SetActive(false);
        player.transform.position = new Vector3(0, y, z + 50); // Обнуление позиции игрока

         player.SetActive(true);
         player.transform.SetParent(null);


    }

    private void generateForLevelMap(int y, int z)
    {
        _instantiateBlocks[r].transform.position = new Vector3(0, _instantiateBlocks[r].transform.position.y + y, _instantiateBlocks[r].transform.position.z + z);
    }

    
}
