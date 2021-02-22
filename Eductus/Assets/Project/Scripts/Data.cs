using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public string nameObject; //Название предмета
    public string nameTeacher; // имя учителя
    public TypeOfObjects typeOfObject; // вид работы [лекция/семинар/лаба]
    public string group; // если это лаба, то к какой группе ты принадлежишь
    public string cabinet; // номер кабинета
    public string data; // дата проведения предмета
    public bool everyWeak; // если true, то предмет идет каждую неделю, иначе -> предмет идет через неделю
    public int onTheAccount; // какая по счету пара
    public bool abbreviationSettings; // использовать сокращения или нет

    public Data(int i)
    {

            cabinet = DataManager.Instance.data[i].cabinet;
            data = DataManager.Instance.data[i].data;
            group = DataManager.Instance.data[i].group;
            typeOfObject = DataManager.Instance.data[i].typeOfObject;
            onTheAccount = DataManager.Instance.data[i].onTheAccount;
            nameTeacher = DataManager.Instance.data[i].nameTeacher;
            nameObject = DataManager.Instance.data[i].nameObject;
            abbreviationSettings = SettingsManager.Instance.abbreviationNames;
        
    }
}
