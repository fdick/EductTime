using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlCanvasController : MonoBehaviour
{
    [SerializeField]
    private DataLvlCnvas lvlCanvas;
    void Start()
    {

        //______________________________________________AWARD_______________________________________________________________________________________________

        transform.Find("AwardObjects").Find("exp_text").GetComponent<Text>().text = lvlCanvas.experienceAward.ToString(); // expirience award
        transform.Find("AwardObjects").Find("gold_text").GetComponent<Text>().text = lvlCanvas.goldAward.ToString(); // gold  award
        if (lvlCanvas.chestNameAward != null)
        {
            transform.Find("AwardObjects").Find("chest_text").GetComponent<Text>().text = lvlCanvas.chestNameAward; // chest name award
        }



         //_____________________________________________REQUIRE____________________________________________________________________________________________



        if (lvlCanvas.skinNameRequire != null)
        {
           // transform.Find("RequireObjects").Find("skin_name").GetComponent<Text>().text = lvlCanvas.skinNameRequire; // skin name require
            var textGO = transform.Find("RequireObjects");
            textGO.Find("lvl_num").GetComponent<Text>().text = lvlCanvas.lvlNumberRequire.ToString(); // lvl require
        }
        


    }


  

}
