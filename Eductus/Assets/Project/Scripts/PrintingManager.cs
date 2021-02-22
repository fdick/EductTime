using System.Collections.Generic;
using UnityEngine;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using iTextSharp.text.pdf.parser;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Net.Http;

public class PrintingManager : MonoBehaviour
{
    public static PrintingManager instance;

    string path = null;
    public string _text;
    public List<string> nakedData = new List<string>();
   [HideInInspector] public byte[] bytes;

    public float Adeltax = 94;
    public float Adeltay = 83;
    public float Bdeltax = 94;
    public float Bdeltay = 78;


  

    string pass = "St000ZsAss%";
    string login = "st118272";


    void Awake()
    {
        instance = this;
        path = "https://drive.google.com/uc?export=download&id=1X_liDjgt5OckEnaTBpvqorc4MJgl6f0F";
   
       

        
    }

    public void StartLoad()
    {
        StartCoroutine(readFile());
    }

    IEnumerator readFile()
    {

        DataManager.Instance.data.Clear();
        TimeTable.instance.RemoveCells();

        UnityWebRequest www = UnityWebRequest.Get(path);
        
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError || www.isModifiable )
        {
            Debug.Log(www.error);
        }
        else
        { 
            TakeDataFromPdf(www);
            EnterManager.instance.enter();
            TimeTable.instance.UpdateWeak();
        }
    }


    void TakeDataFromPdf(UnityWebRequest www)
    {
        MemoryStream mm = new MemoryStream(www.downloadHandler.data);
        bytes = mm.ToArray();
        PdfReader reader = new PdfReader(mm);
        ITextExtractionStrategy strategy;
        for (int q = 0; q < 6; q++)
        {
            for (int j = 0; j < 8; j++)
            {
                Rectangle rect = new Rectangle(45 + (j * Adeltax), 40 + (q * Adeltay), 138 + (j * Bdeltax), 130 + (q * Bdeltay));
                RenderFilter filter = new RegionTextRenderFilter(rect);
                string s;

                strategy = new FilteredTextRenderListener(new SimpleTextExtractionStrategy(), filter);
                s = "";
                s = PdfTextExtractor.GetTextFromPage(reader, 1, strategy);

                //step1_____________________________________

                    if (IsLaba(s))
                    {

                        float microBdeltaX = 0;

                        if (j == 0)
                            microBdeltaX = Bdeltax;

                        Bdeltax *= 2;
                        rect = new Rectangle(45 + (j * Adeltax), 40 + (q * Adeltay), 138 + (j * Bdeltax) + microBdeltaX, 130 + (q * Bdeltay));
                        filter = new RegionTextRenderFilter(rect);
                        strategy = new FilteredTextRenderListener(new SimpleTextExtractionStrategy(), filter);
                        s = PdfTextExtractor.GetTextFromPage(reader, 1, strategy);

                        j++;
                        Bdeltax /= 2;
                    }
                nakedData.Add(s);

            }
        
        }

        mm.Close();

    }

    public bool TakeSomeCellOfData(int x)
    {
        int y = 0;

        x = EnterManager.instance.checkNumberOfLesson(x,ref y);

        MemoryStream mm = new MemoryStream(bytes);
        PdfReader reader = new PdfReader(mm);
        ITextExtractionStrategy strategy;
        RenderFilter filter;
        string s;

  
        Rectangle rect = new Rectangle(45 + (x * Adeltax), 40 + (y* Adeltay), 138 + (x * Bdeltax), 130 + (y * Bdeltay));
        filter = new RegionTextRenderFilter(rect);
        strategy = new FilteredTextRenderListener(new SimpleTextExtractionStrategy(), filter);
        s = PdfTextExtractor.GetTextFromPage(reader, 1, strategy);
        if (s.Contains("семинар") || s.Contains("лекции"))
            return true;
        return false;
    }
    private bool IsLaba(string s)
    {
        if (s.Contains("лабо"))
        {
            return true;
        }
        return false;
    }
    private bool IsSemOrLect(string s)
    {
        if (s.Contains("семи")|| s.Contains("лекц"))
        {
            return true;
        }
        return false;
    }

    IEnumerator readFileFromServer()
    {
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError || www.isModifiable)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }

        HttpClient h = new HttpClient();

        Dictionary<string,string> values = new Dictionary<string,string>
        {
            { "username", "логин" },
            { "password", "пароль" },
            { "login_ok", "" }
        };

        var content = new FormUrlEncodedContent(values);
        var response = h.PostAsync("https://edu.stankin.ru/login/index.php", content);
        
    }

    IEnumerator readAssetBundle()
    {
        AssetBundle assetBundle;
        var download = new WWW("");
        yield return download;
        //find what i want from the package
        assetBundle = download.assetBundle;
       UnityEngine.Object g = assetBundle.LoadAsset("test.pdf");

        path = System.IO.Path.Combine(Application.streamingAssetsPath, "test.pdf");
        FileStream cache = new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
       // cache.Write((byte)assetBundle.mainAsset, 0,(byte)assetBundle.mainAsset.Length);
        cache.Close();

    }

}
