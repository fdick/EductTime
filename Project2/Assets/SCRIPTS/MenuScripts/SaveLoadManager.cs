using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadManager 
{
  
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Data.d";
        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(); //В эту переменную переписываем наше ожидающее сохраниние 

        formatter.Serialize(stream, data); // Сериализуем через этот поток нашу переменную и бинанируем в файл
        stream.Close();
    }
   

    public static Data Load()
    {
        string path = Application.persistentDataPath + "/Data.d";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Data data = formatter.Deserialize(stream) as Data;
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
