using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance = null;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private GameObject levelResult;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        
        HUDPanel.SetActive(true);
        gameOverPanel.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => GameManager.instance.LoadMainMenu());
        gameOverPanel.SetActive(false);
        pauseMenuPanel.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => GameManager.instance.LoadMainMenu());
        pauseMenuPanel.SetActive(false);
        levelCompletedPanel.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => GameManager.instance.LoadMainMenu());
        levelCompletedPanel.SetActive(false);
        SetProgress(0);
    }

    // Update is called once per frame
    void Update()
    {
        SetProgress(player.transform.position.z / mainCamera.GetComponent<LevelCreator>().levelLength);
        
        if (gameOverPanel.activeSelf && Input.GetKey(KeyCode.Return)) {
            mainCamera.GetComponent<LevelCreator>().Restart();
        }
    }

    public bool IsAnyPanelOpened() {
        return gameOverPanel.activeSelf || levelCompletedPanel.activeSelf;
    }

    public void SetGameOverPanel(bool b) {
        gameOverPanel.SetActive(b);
        if (b) mainCamera.GetComponent<CameraMove>().shake = true;
    }

    public void SetProgress(float percent) {
        percent = Mathf.Clamp01(percent);
        if (percent == 1) LevelCompleted();
        
        float total = progressBar.GetComponent<RectTransform>().sizeDelta.y;
        RectTransform progressHandle = progressBar.GetComponentsInChildren<RectTransform>()[1];
        float height = total * percent;
        float posY = height / 2;
        progressHandle.sizeDelta = new Vector2(progressHandle.sizeDelta.x, height);
        progressHandle.anchoredPosition = new Vector2(progressHandle.anchoredPosition.x, posY);
    }

    public void SetPauseMenuPanel(bool b) {
        Time.timeScale = b ? 0 : 1;
        pauseMenuPanel.SetActive(b);
    }

    public bool GetPauseMenuPanel() {
        return pauseMenuPanel.activeSelf;
    }

    public void LevelCompleted() {
        levelCompletedPanel.SetActive(true);
        levelResult.GetComponent<Text>().text = player.GetComponent<PlayerBehavior>().coins.ToString();
        player.GetComponent<PlayerBehavior>().EndGame();
    }
}
