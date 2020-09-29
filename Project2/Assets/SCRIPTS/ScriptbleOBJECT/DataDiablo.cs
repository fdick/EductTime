using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/dataDiablo")]
public class DataDiablo : ScriptableObject
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _speedForce;
    [SerializeField]
    private float _speedDefault;

    public float SpeedDefault { get => _speedDefault; }
    public float SpeedForce { get => _speedForce;}
    public float Speed { get => _speed; set => _speed = value; }
}
