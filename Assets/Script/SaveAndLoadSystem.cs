using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class saveStructure
{
    public int Score;
    public int HP;
    public int Stage;
    public int Column;
    public int Row;
}

public class SaveAndLoadSystem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //use to save score, hp, and stage of player by using PlayerRef
    public void save()
    {
        Game_Manager GM = Game_Manager.Instance;

        if (GM)
        {
            saveStructure save = new saveStructure();

            save.Score = GM.score;
            save.HP = GM.hp;
            save.Stage = GM.stage;
            save.Column = GM.colum;
            save.Row = GM.row;

            string json = JsonUtility.ToJson(save);
            string path = Path.Combine(Application.persistentDataPath, "save.json");
            File.WriteAllText(path, json);

        }

    }

    public void loadSave()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "save.json");
        string json = File.ReadAllText(filePath);
        saveStructure save = JsonUtility.FromJson<saveStructure>(json);
        if (save != null)
        {
            Debug.Log(save.Score);
            Debug.Log(save.HP);
            Debug.Log(save.Stage);

        }

    }

    public saveStructure loadSaveStructure()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "save.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            saveStructure save = JsonUtility.FromJson<saveStructure>(json);
            return save;
        }

        return null;
    }

}
