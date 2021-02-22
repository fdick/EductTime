using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTable : MonoBehaviour
{
    public static TimeTable instance;
    public static MoveTable moveTable;
    public GameObject CellIn;
    public List<GameObject> Cells = new List<GameObject>();
    public Text[] dateText = new Text[6];
    public System.DateTime dt;
    public GameObject CellsTransform;
    string day = "";
    string mounth = "";
    private void Awake()
    {
        instance = this;
        dt = System.DateTime.Today;
        moveTable = FindObjectOfType<MoveTable>();

    }



    void setDate(int n, System.DateTime d)
    {
        dateText[n].text = d.Date.Day < 10 ? "0" + d.Day.ToString() : d.Day.ToString();
        dateText[n].text += ".";
        dateText[n].text += d.Month < 10 ? "0" + d.Month.ToString() : d.Month.ToString();
    }

    void CreateCellAndHisFilling(int i)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Cell"));
        go.transform.SetParent(CellsTransform.transform);
        ObjectData data = go.GetComponent<ObjectData>();
        if (i >= 0)
        {
            data.nameObject = DataManager.Instance.data[i].nameObject;
            data.typeOfObject = DataManager.Instance.data[i].typeOfObject;
            data.group = DataManager.Instance.data[i].group;
            data.cabinet = DataManager.Instance.data[i].cabinet;
            data.data = DataManager.Instance.data[i].data;
            data.nameTeacher = DataManager.Instance.data[i].nameTeacher;
            data.onTheAccount = DataManager.Instance.data[i].onTheAccount;

            if(SettingsManager.Instance.abbreviationNames)
                go.transform.GetChild(0).GetComponent<Text>().text = DefineMicroName(data.nameObject) + "\n" + data.group;
            else
                go.transform.GetChild(0).GetComponent<Text>().text = data.nameObject + "\n" + data.group;

            Text text = go.transform.GetChild(1).GetComponent<Text>();
            text.text = data.typeOfObject.ToString();
            switch (data.typeOfObject)
            {
                case TypeOfObjects.ЛЕКЦИЯ:
                    text.color = Color.green;
                    break;
                case TypeOfObjects.СЕМИНАР:
                    text.color = Color.yellow;
                    break;
                case TypeOfObjects.ЛАБА:
                    text.color = Color.red;
                    break;
                default:
                    break;
            }

        }
        else
        {
            go.transform.GetChild(0).GetComponent<Text>().text = "";
            go.transform.GetChild(1).GetComponent<Text>().text = "";
        }
        Cells.Add(go);

    }

    string DefineMicroName(string fullName)
    {
        if (fullName[0] == ' ') // первый символ может быть пустым, что прервет алгоритм, от него нужно избавиться
           fullName= fullName.Remove(0,1);


        string[] strs = fullName.Split(' ');

        string microName = "";

        for (int i = 0; i < strs.Length; i++)
        {
            if (strs[i].Length > 2)
               strs[i]  = strs[i].ToUpper();

            microName += strs[i][0];
        }
        return microName;
    }
    void DateChecker(string dateOfWeak, int m) // WITH SORT
    {

        for (int l = 1; l <= 8; l++)
        {

            bool brake = false;

            for (int i = 0; i < DataManager.Instance.data.Count; i++)
            {

                if (DataManager.Instance.data[i].data.Contains(dateOfWeak))
                {

                    if (DataManager.Instance.data[i].onTheAccount == l)
                    {
                        CreateCellAndHisFilling(i);
                        brake = true;
                        break;
                    }



                }
                else
                {
                    string[] data = DataManager.Instance.data[i].data.Split(',');

                    for (int e = 0; e < data.Length; e++) // чистка от не нужных символов
                    {
                        data[e] = data[e].Replace("[", "");
                        data[e] = data[e].Replace("]", "");
                        data[e] = data[e].Replace(" ", "");
                    }

                    for (int u = 0; u < data.Length; u++)
                    {
                        if (!data[u].Contains("-"))
                            continue;
                        string start = "";
                        string end = "";
                        string today = dateText[m].text;
                        int index = data[u].IndexOf("-");
                        for (int p = index - 5; p < index; p++) // start
                            start += data[u][p];
                        for (int j = index + 1; j < index + 6; j++) // end
                            end += data[u][j];

                        createDayAndMounth(start);
                        System.DateTime day1Start = new System.DateTime(System.DateTime.Today.Year, int.Parse(mounth), int.Parse(day));

                        createDayAndMounth(end);
                        System.DateTime day2End = new System.DateTime(System.DateTime.Today.Year, int.Parse(mounth), int.Parse(day));

                        createDayAndMounth(today);
                        System.DateTime Today = new System.DateTime(System.DateTime.Today.Year, int.Parse(mounth), int.Parse(day));

                        if (!(Today > day1Start) || !(Today < day2End)) // если текущий день не попадает в диапозон то .. 
                            continue;

                        System.TimeSpan interval = new System.TimeSpan(7, 0, 0, 0);
                        System.TimeSpan interval2 = new System.TimeSpan(0, 0, 0, 0);

                        if (data[u].Contains("к.н"))
                        {
                            for (int h = 0; h < 30; h++)
                            {
                                interval2 += interval;
                                if (Today != day1Start + interval2)
                                    continue;

                                if (DataManager.Instance.data[i].onTheAccount == l)
                                {
                                    CreateCellAndHisFilling(i);
                                    brake = true;
                                    break;
                                }

                            }
                            interval2 = System.TimeSpan.Zero;
                        }
                        else if (data[u].Contains("ч.н"))
                        {
                            interval += new System.TimeSpan(7, 0, 0, 0);
                            for (int h = 0; h < 20; h++)
                            {
                                interval2 += interval;
                                if (Today != day1Start + interval2)
                                    continue;

                                if (DataManager.Instance.data[i].onTheAccount == l)
                                {
                                    CreateCellAndHisFilling(i);
                                    brake = true;
                                    break;
                                }
                            }

                            interval2 = System.TimeSpan.Zero;
                        }
                    }

                }
                if (brake)
                    break;
            }
            if (!brake)
                CreateCellAndHisFilling(-1);
        }

    }

    void createDayAndMounth(string sORe)
    {
        day = sORe.Remove(2, 3);
        mounth = sORe.Remove(0, 3);
        if (mounth[0] == '0')
            mounth = mounth.Remove(0, 1);
        if (day[0] == '0')
            day = day.Replace("0", "");
    }


    public void UpdateWeak()
    {
        int n = 0;
        switch (dt.Date.DayOfWeek)
        {
            case System.DayOfWeek.Friday:
                n = 4;
                break;
            case System.DayOfWeek.Monday:
                n = 0;
                break;
            case System.DayOfWeek.Saturday:
                n = 5;
                break;
            case System.DayOfWeek.Thursday:
                n = 3;
                break;
            case System.DayOfWeek.Tuesday:
                n = 1;
                break;
            case System.DayOfWeek.Wednesday:
                n = 2;
                break;
            default:
                break;
        }
        setDate(n, dt);
        if (dt == System.DateTime.Today)
            dateText[n].color = Color.red;
        else
            dateText[n].color = Color.black;
        //in left
        for (int i = 0; i < dateText.Length - (dateText.Length - n - 1); i++)
        {
            System.TimeSpan interval = new System.TimeSpan(i, 0, 0, 0);
            System.DateTime d = dt.Date.Subtract(interval);
            setDate((dateText.Length - (dateText.Length - n)) - i, d);
        }
        //in right
        int j = 1;
        for (int i = n + 1; i < dateText.Length; i++)
        {
            System.TimeSpan interval = new System.TimeSpan(j, 0, 0, 0);
            System.DateTime d = dt.Date.Add(interval);
            j++;
            setDate(i, d);
        }

        for (int i = 0; i < 6; i++)
        {
            DateChecker(dateText[i].text, i);
        }
        CellIn.SetActive(false);
    }
    public void NextWeak()
    {
        RemoveCells();
        CellIn.SetActive(true); ;
        dt += new System.TimeSpan(7, 0, 0, 0);

        UpdateWeak();
    }
    public void RestartTable()
    {
        RemoveCells();
        CellIn.SetActive(true);
        dt = System.DateTime.Today;
        moveTable.setStartPositionTable((int)dt.DayOfWeek);
        UpdateWeak();
    }
    public void BackWeak()
    {
        RemoveCells();
        CellIn.SetActive(true);
        dt -= new System.TimeSpan(7, 0, 0, 0);

        UpdateWeak();

    }

    public void RemoveCells()
    {
        foreach (var go in Cells)
        {
            Destroy(go);
        }
        Cells.Clear();
    }
}
