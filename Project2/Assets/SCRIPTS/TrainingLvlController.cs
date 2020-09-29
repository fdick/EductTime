using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingLvlController : MonoBehaviour
{

    [SerializeField]
    private int _step;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _InvisiblePanel;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            StopTime(_step);
        }
    }

    private void StopTime(int step) //Остановка времени и вывод анимации
    {
        Time.timeScale = 0;
        _InvisiblePanel.SetActive(true);
        switch (step)
        {
            case 1: _animator.Play("Step1"); break;
            case 2: _animator.Play("Step2"); break;
            case 3: _animator.Play("Step3"); break;
            case 4: _animator.Play("Step4"); break;
            default:
                break;
        }

    }

    public void StartTime()
    {
        Time.timeScale = 1;
        _InvisiblePanel.SetActive(false);
        _animator.Play("Default");

    }
}
