using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnterManager : MonoBehaviour
{
    public static EnterManager instance;
    public Toggle toggleUseSokrasheniya;

    public CellInData cellIn;

    public TimeTable timeTable;
    [SerializeField] string[] nonConstantData = new string[7]; //промежуточный массив

    [SerializeField] int delta = 0; // это счетчик , то есть какая по счету пара в массиве, так как в нативном масиве счетчик считает пару лабу на 1



    private void Awake()
    {
        instance = this;
        nonConstantData = new string[7];
#if UNITY_ANDROID
        Screen.fullScreen = false;
#endif

        Data[] dat = SaveLoadManager.Load();

        if (dat != null)
            for (int i = 0; i < dat.Length; i++)
            {
                ObjectData od = new ObjectData
                {
                    cabinet = dat[i].cabinet,
                    data = dat[i].data,
                    group = dat[i].group,
                    typeOfObject = dat[i].typeOfObject,
                    onTheAccount = dat[i].onTheAccount,
                    nameTeacher = dat[i].nameTeacher,
                    nameObject = dat[i].nameObject
                };
                DataManager.Instance.data.Add(od);
            }

        DataSettings dataSet = SaveLoadManager.LoadSettings();

        if (dataSet != null)
        {
            SettingsManager.Instance.abbreviationNames = dataSet._abbreviationNames;
            toggleUseSokrasheniya.isOn = dataSet._abbreviationNames;
            if(toggleUseSokrasheniya.isOn)
            ChangeAbbreviation();
        }
    }
    private void OnApplicationQuit()
    {
        //SaveLoadManager.SaveSettings();
    }
    private void Start()
    {

        timeTable.UpdateWeak();
    }
    public void ChangeAbbreviation()
    {
        SettingsManager.Instance.abbreviationNames = !SettingsManager.Instance.abbreviationNames;
    }
    public void enter()
    {
        for (int i = 0; i < PrintingManager.instance.nakedData.Count; i++)
        {
            PrintingManager intanse = PrintingManager.instance;

            bool islect = intanse.nakedData[i].Contains("лекции");
            bool islect2 = false;
            if (islect) // если будет 2 записи лекции в одной ячейке
            {
                string str = intanse.nakedData[i];
                str = str.Remove(str.IndexOf("лекции"), 6);
                islect2 = str.Contains("лекции");
            }

            bool issem = intanse.nakedData[i].Contains("семинар");
            bool issem2 = false;
            if (issem) // если будет 2 записи семинар в одной ячейке
            {
                string str = intanse.nakedData[i];
                str = str.Remove(str.IndexOf("семинар"), 7);
                issem2 = str.Contains("семинар");
            }

            bool islab = intanse.nakedData[i].Contains("лабораторные занятия");


            if ((issem && islab) || (issem && islect) || (islab && islect) || islab || issem2 || islect2) // если в ячейке несколько записей
            {



                // 1. Разделяю несколько записей по одной 
                string[] entry = intanse.nakedData[i].Split(']'); //массив записей с одной ячейки

                for (int j = 0; j < entry.Length; j++) // удаление всех кареток для чистоты
                    entry[j] = entry[j].Replace("\n", " ");

                if ((islab && islect) || (islab && issem)) // если кроме лабы в ячейке еще и что то еще
                {
                    for (int o = 0; o < entry.Length; o++)
                    {
                        if (entry[o].Contains("семинар") || entry[o].Contains("лекции"))
                            if (PrintingManager.instance.TakeSomeCellOfData(i))
                                ParseBlockOfData(i + delta, entry[o]);
                            else
                                ParseBlockOfData(i + 1 + delta, entry[o]);
                        else
                            ParseBlockOfData(i + delta, entry[o]);
                    }

                }
                else
                {
                    for (int l = 0; l < entry.Length; l++) // создание объектов из этих записей
                        if (entry[l].Contains(".")) // проверка на то, пуста ли запись или нет
                            ParseBlockOfData(i + delta, entry[l]);
                }

                if (islab)
                    delta++;

            }
            else if (issem || islect)  // если в ячейке всего одна запись то ...
            {
                string str = intanse.nakedData[i].Replace("\n", " "); // удаление всех кареток для чистоты
                ParseBlockOfData(i + delta, str);
            }
            else
            {
                // если ячейка пустая

            }
        }

        SaveLoadManager.Save(); // сохранить данные сразу после загрузки файла
    }
    private void ParseBlockOfData(int q, string entry)
    {
        string s = entry;
        if (s == "" || s == Environment.NewLine || s == " ") // если ячейка даты пуста то выйти
        {
            return;
        }
        string s2 = "";
        int index = 0;
        int countOfDots = 5;

        for (int j = 0; j < countOfDots; j++)
        {

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '.')
                {
                    break;
                }
                s2 += s[i];
                index++;
            }
            if (index + 1 < s.Length)
            {
                s = s.Remove(0, index + 1);
            }

            nonConstantData[j] = s2;

            if (s2.Contains("лабораторные занятия"))
                countOfDots = 6;
            if ((s2.Contains("семинар") || s2.Contains("лекции")) && countOfDots != 3 && countOfDots != 4)
                countOfDots = 5;
            if (s2.Contains("Прикладная физическая культура"))
                countOfDots = 3;
            if (s2 == "Солис П")
                countOfDots = 4;
            if (s2 == "Иностранный язык")
                countOfDots = 3;

            s2 = "";
            index = 0;
            if (j + 1 == countOfDots)
            {
                s = s.Replace(Environment.NewLine, "");
                nonConstantData[j + 1] = s;
            }
        }

        CreateObjectData(q);

    }


    void CreateObjectData(int numberOfLesson)
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.AddComponent<ObjectData>();
        go.name = nonConstantData[0];
        go.transform.parent = gameObject.transform;

        ObjectData od = go.GetComponent<ObjectData>();

        int i = 0; // не для чего не нужно, просто без этого параметра не работает функция
        od.onTheAccount = checkNumberOfLesson(numberOfLesson, ref i);

        if (nonConstantData[0].Contains("Прикладная физическая культура"))
        {
            Fizra(od);
            return;
        }

        od.nameObject = nonConstantData[0];
        od.nameTeacher = nonConstantData[1] + nonConstantData[2];
        switch (nonConstantData[3])
        {
            case " лабораторные занятия":
                od.typeOfObject = TypeOfObjects.ЛАБА;
                break;
            case " лекция":
                od.typeOfObject = TypeOfObjects.ЛЕКЦИЯ;
                break;
            case " семинар":
                od.typeOfObject = TypeOfObjects.СЕМИНАР;
                break;
            default:
                break;
        }
        if (od.typeOfObject == TypeOfObjects.ЛАБА)
        {
            od.group = nonConstantData[4];
            od.cabinet = nonConstantData[5];
            od.data = nonConstantData[6];

            // delta++;
        }
        else
        {
            od.cabinet = nonConstantData[4];
            od.data = nonConstantData[5];
        }

        DataManager.Instance.data.Add(od);
    }

    public int checkNumberOfLesson(int a, ref int ch) // по своей позиции в массиве определяет какая эта пара по счету
    {
        ch = 0;
        for (int i = 0; i < 6; i++)
        {
            if (a - 8 >= 0)
            {
                ch++;
                a -= 8;
                continue;
            }
            return a + 1;
        }

        return 0;
    }
    void Fizra(ObjectData od)
    {
        od.nameObject = nonConstantData[0];
        od.nameTeacher = nonConstantData[2];
        switch (nonConstantData[1])
        {
            case " лекция":
                od.typeOfObject = TypeOfObjects.ЛЕКЦИЯ;
                break;
            case " семинар":
                od.typeOfObject = TypeOfObjects.СЕМИНАР;
                break;
            default:
                break;
        }

        od.data = nonConstantData[3];
     
        DataManager.Instance.data.Add(od);

    }
    void Solis(ObjectData od)
    {
        od.nameObject = nonConstantData[0];
        od.nameTeacher = nonConstantData[1];
        switch (nonConstantData[2])
        {
            case " лекция":
                od.typeOfObject = TypeOfObjects.ЛЕКЦИЯ;
                break;
            case " семинар":
                od.typeOfObject = TypeOfObjects.СЕМИНАР;
                break;
            default:
                break;
        }
        od.cabinet = nonConstantData[3];
        od.data = nonConstantData[4];
        DataManager.Instance.data.Add(od);
    }

    void EnglandLanguage(ObjectData od)
    {
        od.nameObject = nonConstantData[0];

        switch (nonConstantData[1])
        {
            case " лекция":
                od.typeOfObject = TypeOfObjects.ЛЕКЦИЯ;
                break;
            case " семинар":
                od.typeOfObject = TypeOfObjects.СЕМИНАР;
                break;
            default:
                break;
        }

        od.data = nonConstantData[3];
        DataManager.Instance.data.Add(od);
    }

}
