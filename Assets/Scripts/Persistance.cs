using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Persistance: MonoBehaviour{

    public static string saveFileName = "gameState.dat";

    public static void Save(GameStatePersisted gs) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Path.Combine(Application.persistentDataPath, saveFileName));
        bf.Serialize(fs, gs);
        fs.Close();
    }

    public static GameStatePersisted Load()
    {

        if(!File.Exists(Path.Combine(Application.persistentDataPath, saveFileName)))
        {
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Path.Combine(Application.persistentDataPath, saveFileName),FileMode.Open);
        var gs = (GameStatePersisted)bf.Deserialize(fs);
        fs.Close();
        return gs;
    }


    public static void Remove()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, saveFileName)))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, saveFileName));
        }
    }

    public static GameStatePersisted Merge(GameStatePersisted gs)
    {
        GameStatePersisted saved = Load();
        if (saved == null)
        {
            return gs;
        }

        GameStatePersisted merged = gs; //this will copy every attribute which is not important to merge and must be always rewritten

        merged.lastCheckpoint = gs.lastCheckpoint == null ? saved.lastCheckpoint : gs.lastCheckpoint;
        merged.lastLevelCheckpoint = gs.lastLevelCheckpoint == null ? saved.lastLevelCheckpoint : gs.lastLevelCheckpoint;

        return merged;
    }

}
