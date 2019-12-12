using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public int currentLevel { get; private set; } = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else if (instance != this) {
            Destroy(gameObject);
        }
        
        GameObject.Find("Level1Button").GetComponent<Button>().onClick.AddListener(() => instance.StartLevel(1));
        GameObject.Find("Level2Button").GetComponent<Button>().onClick.AddListener(() => instance.StartLevel(2));
        GameObject.Find("Level3Button").GetComponent<Button>().onClick.AddListener(() => instance.StartLevel(3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel (int lvl)
    {
        currentLevel = lvl;
        SceneManager.LoadScene("Level");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
