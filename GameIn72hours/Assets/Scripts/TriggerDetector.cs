using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{

    private CharacterControl owner;

    private void Awake()
    {
        owner = this.GetComponentInParent<CharacterControl>();
    }

    private void OnTriggerEnter(Collider col)
    {

        //if (owner.RagdollParts.Contains(col))
        //{
        //    Debug.Log("return1");
        //    return;
        //}

        CharacterControl attacker = col.transform.root.GetComponent<CharacterControl>(); // this is a victim

        if (attacker == null)
        {
            //Debug.Log("VICTIM is NULL " + transform.root.gameObject);
            return;
        }
        else
        {
            Debug.Log("VICTIM IS " + attacker.transform.root.name + " says: " + transform.root.name);
        }

        if (col.gameObject == attacker.gameObject)
        {

            Debug.Log(" I TOUCH " + attacker.transform.root.name + " BIG box collider  " + transform.root.gameObject);
            return;
        }

        //if (!owner.CollidingParts.Contains(col))
        //{
        //    Debug.Log("ENTER THE COLLIDER " + transform.root.name);
        //    owner.CollidingParts.Add(col);
        //}
    }

    //private void OnTriggerExit(Collider attacker)
    //{
    //    if (owner.CollidingParts.Contains(attacker))
    //    {
    //        owner.CollidingParts.Remove(attacker);
    //    }
    //}
}
