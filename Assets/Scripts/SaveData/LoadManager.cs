using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class LoadManager
{
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.me";

        ProgressData progressData = new ProgressData(progress);

        FileStream file = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(file, progressData);
        file.Close();
    }

    public static ProgressData Load()
    {
        string path = Application.persistentDataPath + "/progress.me";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);
            ProgressData progress = binaryFormatter.Deserialize(file) as ProgressData;
            file.Close();
            return progress;
        }
        else
        {
            return null;
        }
    }

    public static void Delete()
    {
        string path = Application.persistentDataPath + "/progress.me";
        File.Delete(path);
    }
}
