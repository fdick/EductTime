using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorldTrigger : MonoBehaviour
{
    public GameObject anotherWorldBLOCK;
    public GameObject deleteThisWorldBLOCK;


    public void ChangeBlocks(bool _change)
    {
        StartCoroutine(WaitALittle(_change));
    }

    public IEnumerator WaitALittle(bool _change)
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("switch the world");
        if (deleteThisWorldBLOCK != null)
        {
           
            deleteThisWorldBLOCK.SetActive(_change);
            
        }
        _change = !_change;
        if (anotherWorldBLOCK != null)
        {
            anotherWorldBLOCK.SetActive(_change);
        }
       
    }

    
}
