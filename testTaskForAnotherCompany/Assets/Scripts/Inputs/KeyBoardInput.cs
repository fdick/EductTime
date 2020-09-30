using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            InputManager.move = true;
        else
            InputManager.move = false;

        if (Input.GetKey(KeyCode.A))
            InputManager.rotateLeft = true;
        else
            InputManager.rotateLeft = false;

        if (Input.GetKey(KeyCode.D))
           InputManager.rotateRight = true;
        else
           InputManager.rotateRight = false;

        if (Input.GetKeyUp(KeyCode.Mouse0))
            InputManager.attack = true;

 

    }
}
