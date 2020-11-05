using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // following code is taken from a tutorial video by Brackeys: https://www.youtube.com/watch?v=XOjd_qU2Ido 
    public static void SaveLevel(GameManager gm)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shapeescape.wws";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(gm);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevel()
    {
        string path = Application.persistentDataPath + "/shapeescape.wws";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            SaveLevel(GameManager.instance);
        }
        return null;
    }
}
