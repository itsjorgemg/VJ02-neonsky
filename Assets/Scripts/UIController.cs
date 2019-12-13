using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance = null;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private GameObject progressBar;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        
        gameOverPanel.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => GameManager.instance.LoadMainMenu());
        gameOverPanel.SetActive(false);
        HUDPanel.SetActive(true);
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

    public void SetGameOverPanel(bool b) {
        gameOverPanel.SetActive(b);
        if (b) mainCamera.GetComponent<CameraMove>().shake = true;
    }

    public void SetProgress(float percent) {
        percent = Mathf.Clamp01(percent);
        float total = progressBar.GetComponent<RectTransform>().sizeDelta.y;
        RectTransform progressHandle = progressBar.GetComponentsInChildren<RectTransform>()[1];
        float height = total * percent;
        float posY = height / 2;
        progressHandle.sizeDelta = new Vector2(progressHandle.sizeDelta.x, height);
        progressHandle.anchoredPosition = new Vector2(progressHandle.anchoredPosition.x, posY);
    }
}
