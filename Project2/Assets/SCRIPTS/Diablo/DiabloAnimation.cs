using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiabloAnimation : MonoBehaviour
{
    [HideInInspector]
    public Animator _animator;
    private DiabloController _charController;
    private DiabloSwitchControllerToPodkat _switchToControllerSCR;
    public float time_to_podkat;
    public Transform ParentDiablo;

    

    void Start()
    {
     
        _charController = gameObject.transform.parent.GetComponent<DiabloController>();
        _animator = GetComponent<Animator>();
        _switchToControllerSCR = GetComponent<DiabloSwitchControllerToPodkat>();
    }

   

   

    public void isGroundfunc()
    {
        if (_charController._charController.isGrounded)
        {
            _animator.SetBool("jump", false);
            _charController.Ijumping = false;

        }

    }

    public void isNotGroundfunc()
    {
        if (!_charController._charController.isGrounded)
        {
            _animator.SetBool("jump", true);
            

        }

    }

    public void podkatfunc()
    {
        if (_charController._charController.isGrounded)
        {
            _animator.SetBool("podkat", true);
            StartCoroutine(podkatCoroutine());
            
          
        }


    }
    public IEnumerator podkatCoroutine ()
    {
            yield return new WaitForSeconds(time_to_podkat);
        _animator.SetBool("podkat", false);
            _charController.canJump = true;
        _charController.canPodkat = true;


        // Обнуление парент позиции ///////////////////////////////////////////////////////////////////////

        var z = ParentDiablo.localPosition.z;
        var y = ParentDiablo.localPosition.y;

        ParentDiablo.localPosition = new Vector3(0, 0, 0);
        _charController.GODiablo.position = new Vector3(_charController.GODiablo.position.x,
                                                         _charController.GODiablo.position.y + y,
                                                         _charController.GODiablo.position.z + z);

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        if (_switchToControllerSCR.alone)
        {
            _charController.SwitchCollider();
        }

            
        
    }

        
}
