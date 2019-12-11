using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private int currentlvl = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel (int lvl)
    {
        currentlvl = lvl;
        SceneManager.LoadScene("Level");
    }

    public int GetCurrentLevel ()
    {
        return currentlvl;
    }
}
