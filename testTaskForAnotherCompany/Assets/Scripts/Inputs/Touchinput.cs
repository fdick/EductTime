using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchinput : MonoBehaviour
{
    public void MoveDown()
    {
        InputManager.move = true;
    }
    public void MoveUp()
    {
        InputManager.move = false;
    }

    public void FireDown()
    {
        InputManager.attack = true;
    }
    public void FireUp()
    {
        InputManager.attack = false;
    }


    public void RotateLeftDown()
    {
        InputManager.rotateLeft = true;
    }
    public void RotateLeftUp()
    {
        InputManager.rotateLeft = false;
    }
    public void RotateRightDown()
    {
        InputManager.rotateRight = true;
    }
    public void RotateRightUp()
    {
        InputManager.rotateRight = false;
    }
}
