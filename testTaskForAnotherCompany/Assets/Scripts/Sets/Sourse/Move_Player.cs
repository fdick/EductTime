using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "move", menuName = "Objects/Actions/Move")]
public class Move_Player : Actions
{
    [SerializeField] private float speed;
   // [SerializeField] private float inertia;
    public override void Attack(ref float originTime, GameObject bullet, Transform player, Transform origin)
    {
      //  throw new System.NotImplementedException();
    }

    public override void Move( Transform transform)
    {
        if (InputManager.move)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
       
    }

    public override void Rotate(Transform transform)
    {
        //throw new System.NotImplementedException();
    }
}
