using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "attack", menuName = "Objects/Actions/Attack")]
public class Attack_Player : Actions
{
    [SerializeField] private float Cd; // кулдаун выстрела


    public override void Attack(ref float originTime, GameObject bullet, Transform player, Transform origin)
    {
        if (InputManager.attack)
        {
            if (Time.time >= originTime + Cd)
            {
                //fire  
               
                bullet.transform.position = origin.position;
                bullet.transform.rotation = player.rotation;
                bullet.SetActive(true);
                originTime = Time.time;
            }
            InputManager.attack = false;
        }
    }

    public override void Move(Transform transform)
    {
        //  throw new System.NotImplementedException();
    }

    public override void Rotate(Transform transform)
    {
        //  throw new System.NotImplementedException();
    }
}