using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveDat
{
    
    public static void SaveDecks (Decks decks)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.text";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerDat dat = new PlayerDat(decks);

        formatter.Serialize(stream, dat);
        stream.Close();
    }

    public static PlayerDat LoadDecks()
    {
        string path = Application.persistentDataPath + "/player.text";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerDat dat = formatter.Deserialize(stream) as PlayerDat;

            stream.Close();
            return dat;
        }
        return null;
    }

}
