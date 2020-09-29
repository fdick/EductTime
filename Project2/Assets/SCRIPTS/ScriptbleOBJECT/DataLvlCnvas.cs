using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/dataLevel Canvas")]
public class DataLvlCnvas : ScriptableObject
{
    public Image artImage;
    public int lvlNumberRequire;
    public string skinNameRequire;

    public int experienceAward;
    public int goldAward;
    public string chestNameAward;


}
