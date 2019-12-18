using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public int currentLevel { get; private set; } = 1;

    [SerializeField] private GameObject godModePanel;
    private bool godMode = false;

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
        GameObject.Find("ExitButton").GetComponent<Button>().onClick.AddListener(() => Application.Quit());
        DontDestroyOnLoad(godModePanel.transform.parent.gameObject);
        godModePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            godMode = !godMode;
            godModePanel.SetActive(godMode);
        }
        if (godMode) {
            PlayerBehavior pl = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();
            if (Input.GetKeyDown(KeyCode.Space)) pl.paused = !pl.paused;
            godModePanel.GetComponentsInChildren<Text>()[2].text = pl.paused.ToString();

            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKey(KeyCode.X)) {
                UIController.instance.SetGameOverPanel(false);
                UIController.instance.SetPauseMenuPanel(false);
            }
            if (Input.GetKey(KeyCode.X)) {
                Transform tr = GameObject.FindWithTag("Player").transform;
                tr.position = new Vector3(Mathf.Clamp(tr.position.x, 0.0f, 4.0f), 0.3f, tr.position.z);
                pl.paused = false;
                StartCoroutine(tr.GetComponent<PlayerBehavior>().Fade(false));
            }
            if (Input.GetKeyDown(KeyCode.C)) {
                pl.speedForward *= 2;
            }
            if (Input.GetKeyUp(KeyCode.C)) {
                pl.speedForward /= 2;
            }
        }
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
