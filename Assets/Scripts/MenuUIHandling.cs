using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Hosting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;


public class MenuUIHandling : MonoBehaviour
{
    public TMP_InputField inputName;
    static public MenuUIHandling InstanceMenu;

    public Text playerName;
    public string bestScore;
    public string currentPlayer;

    public void Awake()
    {
        if(InstanceMenu != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceMenu = this;
        DontDestroyOnLoad(gameObject);
        inputName.text = "Default Name";
        LoadProgress();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = "Name :" + playerName.text + "\n" + "Has" + bestScore;
    }

    public void OnStartClicked()
    {
        InstanceMenu.currentPlayer = inputName.text;

    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    

    public void Exit()
    {
#if UNITY_EDITOR
      EditorApplication.ExitPlaymode();

#else
        Application.Quit();

#endif

    }
    [System.Serializable]
    class SaveData
    {
        public string bestScore;
        public string playerName;
    }


        public void SaveProgress()
        {
            SaveData data = new SaveData();
            data.playerName = InstanceMenu.currentPlayer;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
        }

        public void LoadProgress()
        {
            string path = Application.persistentDataPath + "/bestscore.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                InstanceMenu.playerName.text = data.playerName;
                InstanceMenu.bestScore = data.bestScore;
            }
        }
    
}
