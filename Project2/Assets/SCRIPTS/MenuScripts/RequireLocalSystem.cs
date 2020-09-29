using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequireLocalSystem : MonoBehaviour
{

    private Animator animator;

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private XPsystem xp_system;
    [SerializeField]
    private GameObject _plashka; // Помоему это не нужно тут


    [SerializeField]
    private int requireLevel;
    [SerializeField]
    private DataRecord _lastLvlDataRecords;





    void Start()
    {
        animator = GetComponent<Animator>();
        ReqLevel(requireLevel);
        ReqRecord();
    }

   public void ReqLevel(int requireLevel)  // Если уровень маленький, то играть нельзя
    {
        if (xp_system.GetLevel() < requireLevel)  
        {
            _playButton.interactable = false;
            _plashka.SetActive(true);
        }
    }

    public void ReqRecord() // Если рекорд прошлого лвла не побит, то играть нельзя
    {
        if (_lastLvlDataRecords!= null && _lastLvlDataRecords.names[0] != "YOU" && _lastLvlDataRecords != null)
        {
            _playButton.interactable = false;
            _plashka.SetActive(true);
        }
    }

    public void UsePlashka()
    {
        if (xp_system.GetLevel() < requireLevel)  // Анимация лвл'а
        {
            animator.Play("RequireObjects_Lvl");
            print("use_plashka");
            animator.SetBool("bool1", true);
        }
    }

    public void RecordReq_Animation()  // Анимация рекорда
    {
        if (_lastLvlDataRecords != null && _lastLvlDataRecords.names[0] != "YOU")
        {
            animator.Play("RequireObjects_Skin");
            print("recordReq");
            animator.SetBool("bool2", true);
        }

    }
    public void Both_Animation()  // Анимация обоих
    {
        if (animator.GetBool("bool1") && animator.GetBool("bool2"))
        {
            animator.Play("RequireObjects_Lvl");
            
        }

    }


   public void time()
    {
      
        animator.SetBool("bool2", false);
        animator.SetBool("bool1", false);
    }
    

    
}
