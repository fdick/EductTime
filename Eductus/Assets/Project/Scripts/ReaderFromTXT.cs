using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;



public class ReaderFromTXT : MonoBehaviour
{
    static ReaderFromTXT instance;

    public string[] data;
    public string[] data2;
    public PrintingManager printingManager;

    public static ReaderFromTXT Instance { get => instance; set => instance = value; }

    //public FileExplorer file;
    void Awake()
    {
        Instance = this;
      //  StartRead();
    }

    public void StartRead()
    {
        TextAsset questData = Resources.Load<TextAsset>("idb1806.pdf");
        print(questData);
        data2 = printingManager._text.Split(new string[] { "]" }, System.StringSplitOptions.None);
        //data = file.content.Split(new string[] { "]" }, System.StringSplitOptions.None);
        data = questData.text.Split(new string[] { "]" }, System.StringSplitOptions.None);
        //for (int i = 0; i < data.Length; i++)
        //{
        //    print(data[i]);
        //}
    
        //print("__________________________________________________________________");
        //for (int i = 0; i < data2.Length; i++)
        //{
        //    print(data2);
        //}
       
      //  FirstTime();
    }
    void FirstTime() // удаляет первые три строки
    {
        string[] q = data[0].Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
        string s = "";
        for (int i = 3; i < q.Length; i++)
        {
            s += q[i];
        }
        data[0] = s;
        
    }

    void Print()
    {
        for (int i = 0; i < data.Length; i++)
        {
            print(data[i]);
        }
    }
}

