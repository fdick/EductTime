using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectData : MonoBehaviour
{
    public string nameObject; //Название предмета
    public string nameTeacher; // имя учителя
    public TypeOfObjects typeOfObject; // вид работы [лекция/семинар/лаба]
    public string group; // если это лаба, то к какой группе ты принадлежишь
    public string cabinet; // номер кабинета
    public string data; // дата проведения предмета
    public int onTheAccount; // какая по счету пара



    public void WhichOfObject(ref int allPars, ref int currentPar)
    {
       
        
        for (int j = 0; j < DataManager.Instance.data.Count; j++)
        {
            float allParsLocal = 0;
            float currentParLocal = 0;
            if (!DataManager.Instance.data[j].nameObject.Contains(nameObject) || !DataManager.Instance.data[j].typeOfObject.Equals(typeOfObject))
                continue;

            if (typeOfObject == TypeOfObjects.ЛАБА && !DataManager.Instance.data[j].group.Contains(group))
                continue;


            string[] strs = DataManager.Instance.data[j].data.Split(',');
                

            for (int o = 0; o < strs.Length; o++)
            {
               
                 bool ch = false;
                ClearData(ref strs[o], ref ch);
                if (strs[o].Length < 2)
                    continue;
                string startDay = strs[o][0].ToString() + strs[o][1];
                string startMounth = strs[o][3].ToString() + strs[o][4];

                System.DateTime startDate = new System.DateTime(System.DateTime.Today.Year, int.Parse(startMounth), int.Parse(startDay));
                if (strs[o].Contains("-"))
                {
                    string endDay = strs[o][6].ToString() + strs[o][7];
                    string endMounth = strs[o][9].ToString() + strs[o][10];

                    System.DateTime endDate = new System.DateTime(System.DateTime.Today.Year, int.Parse(endMounth), int.Parse(endDay));
                    System.TimeSpan interval = new System.TimeSpan(7, 0, 0, 0);
                    System.TimeSpan deltaInterval = new System.TimeSpan(0, 0, 0, 0);
                    for (int u = 0; u < 15; u++)
                    {
                        if (startDate + deltaInterval <= endDate)
                        {
                            allParsLocal++;
                            if (startDate + deltaInterval <= TimeTable.instance.dt)
                                currentParLocal++;

                            deltaInterval += interval;
                        }
                    }
                    if (ch)
                    {
                        allParsLocal /= 2.0f;
                        allParsLocal = Mathf.CeilToInt(allParsLocal);
                        currentParLocal /= 2.0f;
                        currentParLocal = Mathf.CeilToInt(currentParLocal);
                        ch = false;
                    }
                 
                }
                else
                {
                    allPars++;
                    if (startDate <= TimeTable.instance.dt)
                        currentPar++;
                }
            }
            allPars += (int)allParsLocal;
            currentPar += (int)currentParLocal;

            for (int p = 0; p < strs.Length; p++) // чистка стрс так как там остается мусор
                strs[p].Remove(0);
        }
        //print(currentPar + "/" + allPars);
    }
    void ClearData(ref string st, ref bool ch)
    {
        if (st.Contains("к") || st.Contains("ч"))
        {
            if (st.Contains("ч"))
                ch = true;
            st = st.Remove(st.IndexOf("н") - 2, 4);
            
        }
       st = st.Replace("[", "");
       st = st.Replace("]", "");
       st = st.Replace(" ", "");
       st = st.Replace("\n", "");

    }

}
