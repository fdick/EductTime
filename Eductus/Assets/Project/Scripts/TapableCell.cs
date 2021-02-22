using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapableCell : MonoBehaviour, IPointerClickHandler
{
    GameObject panel;
    ObjectData data;
    CellInData dataInCellTexts;
    private void Awake()
    {
        dataInCellTexts = EnterManager.instance.cellIn;
        data = GetComponent<ObjectData>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        int all = 0;
        int current = 0;
        EnterManager.instance.cellIn.gameObject.SetActive(true);
        dataInCellTexts.texts[0].text = data.nameObject;
        dataInCellTexts.texts[1].text = data.cabinet;
        dataInCellTexts.texts[2].text = data.nameTeacher;
        dataInCellTexts.texts[3].text = data.data;
        dataInCellTexts.texts[4].text = data.typeOfObject.ToString();
        dataInCellTexts.texts[5].text = data.group;
        if (data.nameObject.Length > 4) // чтобы в пустых клетках не расчитывалось значение
        {
            data.WhichOfObject(ref all, ref current);
            dataInCellTexts.texts[7].text = current.ToString() + '/' + all.ToString();
        }
        dataInCellTexts.dropDown.value = data.onTheAccount;

        dataInCellTexts.objectData = data;
    }
}
