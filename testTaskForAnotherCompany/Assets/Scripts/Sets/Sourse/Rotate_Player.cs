using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "rotate", menuName = "Objects/Actions/Rotate")]
public class Rotate_Player : Actions
{
    [SerializeField] private float Angle;
    private Quaternion q1 = new Quaternion();
    public override void Attack(ref float originTime, GameObject bullet, Transform player, Transform origin)
    {
        //  throw new System.NotImplementedException();
    }

    public override void Move(Transform transform)
    {
        //  throw new System.NotImplementedException();
    }

    public override void Rotate(Transform transform)
    {
     
        if (InputManager.rotateLeft)
        {
            q1 = Quaternion.AngleAxis(-Angle, Vector3.up);
            transform.rotation *= q1;
        }
        if (InputManager.rotateRight)
        {
            q1 = Quaternion.AngleAxis(Angle, Vector3.up);
            transform.rotation *= q1;
        }

    }
}