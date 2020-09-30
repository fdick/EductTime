using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions : ScriptableObject
{
    public abstract void Move(Transform transform);
    public abstract void Rotate(Transform transform);
    public abstract void Attack(ref float originTime, GameObject bullet, Transform player, Transform origin);

}
