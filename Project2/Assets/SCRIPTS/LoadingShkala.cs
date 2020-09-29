using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingShkala : MonoBehaviour
{
    
    [SerializeField]
    private Image _shkala;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private Sprite[] imagesLoading;
    [SerializeField]
    private RawImage _diabloImage;
    [SerializeField]
    private GlobalFunctions _G_func;
    [SerializeField]
    private DiabloController _player;


  

    public void LoadLevel(int index)
    {
        
        gameObject.SetActive(true);
        StartCoroutine(LoadAsync(index));
    }

    IEnumerator LoadAsync(int index)
    {
       
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
    
            _shkala.fillAmount = Mathf.MoveTowards(_shkala.fillAmount, async.progress, Time.deltaTime);
            if (_shkala.fillAmount < 0.1f)
            {
                _diabloImage.texture = imagesLoading[0].texture;
            }
           else if (_shkala.fillAmount < 0.2f)
            {
                _diabloImage.texture = imagesLoading[1].texture;
            }
           else if (_shkala.fillAmount < 0.3f)
            {
                _diabloImage.texture = imagesLoading[2].texture;
            }
           else if (_shkala.fillAmount < 0.4f)
            {
                _diabloImage.texture = imagesLoading[3].texture;
            }
           else if (_shkala.fillAmount < 0.5f)
            {
                _diabloImage.texture = imagesLoading[4].texture;
            }
           else if (_shkala.fillAmount < 0.6f)
            {
                _diabloImage.texture = imagesLoading[5].texture;
            }
          else  if (_shkala.fillAmount < 0.7f)
            {
                _diabloImage.texture = imagesLoading[6].texture;
            }
            else if (_shkala.fillAmount < 0.8f)
            {
                _diabloImage.texture = imagesLoading[7].texture;
            }

           

            if (async.progress == 0.9f )
            {
                 _shkala.fillAmount = 1;
                _diabloImage.texture = imagesLoading[8].texture;
                if (_shkala.fillAmount == 1)
                {
                    async.allowSceneActivation = true;
                    Time.timeScale = 1;
                }
                

            }
            yield return null;
        }
        
        
    }

   public void OffWinPanel()
    {
    
            winPanel.SetActive(false);
        
        
    }
    
    public void OffLosePanel()
    {

     

        losePanel.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 2 && _player.live)
        {
            DiabloController.deathEvent -= _G_func.multi;
           
        }
        if (_player.live)
        {
            DiabloController.deathEvent -= _G_func.multi_G;
        }
        


    }
  
}
