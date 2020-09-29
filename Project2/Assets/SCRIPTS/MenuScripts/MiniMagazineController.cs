using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMagazineController : MonoBehaviour
{
    private Text _stamina_text;
    private Text _revival_text;

    [SerializeField]
    private GOLDsystem goldSytem;
    [SerializeField]
    private GameObject goldBar;

    public AudioSource buy_sound;


    



    void Start()
    {
        _stamina_text = transform.Find("10").GetComponent<Text>();
        _revival_text = transform.Find("1").GetComponent<Text>();




        _stamina_text.text = Gold_manager.stamina.ToString();
        _revival_text.text = Gold_manager.revival.ToString();

    }

    public void BuyStamina()
    {
        if (goldSytem.gold -200 >= 0 && int.Parse(_stamina_text.text) + 10 <= 100)
        {
            buy_sound.Play();
            goldSytem.awardGold = -200;
            goldSytem.MinusGold();

           _stamina_text.text = (int.Parse(_stamina_text.text) + 10).ToString();   // Если меньше 100, то добавлть стамину
            Gold_manager.stamina = int.Parse(_stamina_text.text); // сохраняем в менеждере количество сатмины чтобы передать на уровень

        }
      
    }

    public void BuyRevival()
    {
        if (goldSytem.gold - 600 >= 0 && int.Parse(_revival_text.text) + 1 <= 3)
        {
            buy_sound.Play();
            goldSytem.awardGold = -600;
            goldSytem.MinusGold();

            _revival_text.text = (int.Parse(_revival_text.text) + 1).ToString();   // Если меньше 100, то добавлть стамину
            Gold_manager.revival = int.Parse(_revival_text.text);

        }
    }

    public void OnPlashka_for_Magazine()
    {
        gameObject.SetActive(true);
        goldBar.GetComponent<Canvas>().sortingOrder = 1; // голд бар становится поверх всех окон
        print("Open Magazine");
    }

    public void OffMagazine()
    {
        gameObject.SetActive(false);
        goldBar.GetComponent<Canvas>().sortingOrder = -1;
        print("OffMagazine");
    }
}
