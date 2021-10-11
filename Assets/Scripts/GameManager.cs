using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string bestPlayerName;
    public int bestPlayerScore;

    public Text gameTitle;
    public Text playerNameUI;

    public string playerName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            //return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
        gameTitle = GameObject.Find("Title").GetComponentInChildren<Text>();
        playerNameUI = GameObject.Find("Input").GetComponentInChildren<Text>();
        if (bestPlayerName != "")
        {
            gameTitle.text = "Best Score - "+ bestPlayerName + " : " + bestPlayerScore;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestPalyerScore;
    }

    public void SaveBestScore(string playerName, int playerScore)
    {
        SaveData data = new SaveData();
        data.bestPlayerName = playerName;
        data.bestPalyerScore = playerScore;

        string json = JsonUtility.ToJson(data);
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            bestPlayerScore = data.bestPalyerScore;
        }
    }
}
