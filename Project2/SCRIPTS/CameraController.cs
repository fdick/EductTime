using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.PostProcessing;

public class CameraController : MonoBehaviour
{
    public float thresSpeed;
    public  int ghostEffectInt; // on / off ghost effect

    [SerializeField]
    public Animator _animator;


    void Start()
    {
        ghostEffectInt = 0;
    }

    void Update()
    {
        GhostEffectIMPULSE(ghostEffectInt);
        GhostEffect(ghostEffectInt);
        GhostEffectnDefault(ghostEffectInt);

        

    }

    public void GhostEffectIMPULSE(int o)
    {
        if (o == 1)
        {
            _animator.SetBool("Start" , true);  

        }   

    }

    public void GhostEffect(int o)
    {
        if (o == 2)
        {
            //Nothing
        }

    }

    public void GhostEffectnDefault(int o)
    {
        if (o == 3)
        {

            _animator.SetBool("Start", false);


        }

    }




}
