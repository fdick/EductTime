using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{

    AudioSource music;
    [SerializeField] private LevelController lvlCtrl;
   [SerializeField] bool changeLevel = false;

    [SerializeField] private AudioSource[] clips;
    [SerializeField] DiabloController player;
  

    int a = 0;
    int b = 0;
    int c = 0;
    

    void Start()
    {
        music = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (lvlCtrl.level == LevelController.eLevels.Medium)
        {
            if (a == 0)
            {
                changeLevel = true;
                
                a++;
            }

            
        }
        if (lvlCtrl.level == LevelController.eLevels.Hard)
        {
            if (a == 1)
            {
                changeLevel = true;
                a++;
            }


        }

        if (player.live)
        {


            if (changeLevel && b == 0)
            {
                print("startMute");
                StartCoroutine(muteMusic());
                b++;
            }
            else if (music.volume <= 0 && c == 1)
            {
                StopCoroutine(muteMusic());
                print("startUnMute");

                StartCoroutine(UnmuteMusic());
                

            }
        }
        else
        {
            StopCoroutine(UnmuteMusic());
            StartCoroutine(muteMusic());
        }
    }


    public IEnumerator UnmuteMusic()
    {
        music.mute = false;
       
        music.volume = Mathf.MoveTowards(music.volume, 0.8f, 0.2f);
        yield return new WaitForSeconds(0.7f);
        if (music.volume < 0.8f)
        {
            StartCoroutine(UnmuteMusic());
        }
        else
        {
            music.volume = 0.8f;
            c = 0;
            b = 0;
            

        }
     

    }
    public IEnumerator muteMusic()
    {
      
        changeLevel = false;
        music.volume = Mathf.MoveTowards(music.volume, 0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        if (music.volume > 0)
        {
            StartCoroutine(muteMusic());
        }
        else
        {
            music.mute = true;
            music.clip = clips[a-1].clip;
            music.Play();
            c++;
        }
       
     
    }

}
