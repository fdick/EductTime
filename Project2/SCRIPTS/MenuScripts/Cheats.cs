using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [SerializeField]
    private GOLDsystem goldSystem;
    [SerializeField]
    private XPsystem xpSystem;


    void Start()
    {
        //goldSystem.awardGold = 10000;
        //goldSystem.AddGold();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            goldSystem.awardGold = 5000;
            goldSystem.AddGold();
            //Gold_manager.currentGold += 5000;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            xpSystem.awardXp = 50000;
            xpSystem.AwardValue(1000);
        }
    }
}
