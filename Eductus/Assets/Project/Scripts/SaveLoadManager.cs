using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{

    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveDataEductus.d";
        FileStream stream = new FileStream(path, FileMode.Create);

        //  Data data = new Data(); //В эту переменную переписываем наше ожидающее сохраниние 
        Data[] data = new Data[DataManager.Instance.data.Count];

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new Data(i);
        }


        formatter.Serialize(stream, data); // Сериализуем через этот поток нашу переменную и бинанируем в файл
        stream.Close();
    }  
    public static void SaveSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SavesSettingsEductus.d";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataSettings data = new DataSettings();

        formatter.Serialize(stream, data); // Сериализуем через этот поток нашу переменную и бинанируем в файл
        stream.Close();
    }


    public static Data[] Load()
    {
        string path = Application.persistentDataPath + "/SaveDataEductus.d";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Data[] data = formatter.Deserialize(stream) as Data[];
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static DataSettings LoadSettings()
    {
        string path = Application.persistentDataPath + "/SavesSettingsEductus.d";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataSettings data = formatter.Deserialize(stream) as DataSettings;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

  


}
