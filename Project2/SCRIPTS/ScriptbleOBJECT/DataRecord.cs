using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/dataRecords")]
public class DataRecord : ScriptableObject
{
    public string[] names;
    public float[] scoresTime;
    public int levelNumber;


}
