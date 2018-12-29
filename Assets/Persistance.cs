using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Persistance: MonoBehaviour{

    public static string saveFileName = "gameState.dat";

    public static void Save(GameState gs) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Path.Combine(Application.persistentDataPath, saveFileName));
        bf.Serialize(fs, gs);
        fs.Close();
    }

    public static GameState Load()
    {

        if(!File.Exists(Path.Combine(Application.persistentDataPath, saveFileName)))
        {
            return null;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(Path.Combine(Application.persistentDataPath, saveFileName),FileMode.Open);
        var gs = (GameState)bf.Deserialize(fs);
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

    public static GameState Merge(GameState gs)
    {
        GameState saved = Load();
        if (saved == null)
        {
            return gs;
        }

        GameState merged = new GameState();

        merged.lastCheckpoint = gs.lastCheckpoint == null ? saved.lastCheckpoint : gs.lastCheckpoint;
        merged.lastLevelCheckpoint = gs.lastLevelCheckpoint == null ? saved.lastLevelCheckpoint : gs.lastLevelCheckpoint;

        return merged;
    }

}
