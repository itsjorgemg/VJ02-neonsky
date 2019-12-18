using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelCreator : MonoBehaviour
{

    [SerializeField] private FileInfo sourceFile = new FileInfo("Assets/Levels/Level1.txt");
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private GameObject[] collectibles;
    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject mainCamera;

    public int levelLength { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
            sourceFile = new FileInfo("Assets/Levels/Level" + GameManager.instance.currentLevel + ".txt");
        
        StreamReader reader = sourceFile.OpenText();
        string text = reader.ReadLine();

        for (int i = 0; text != null; ++i) {
            createRow(i, text);
            text = reader.ReadLine();
            levelLength = i;
        }
        switch (GameManager.instance.currentLevel)
        {
            case 1:
                mainCamera.GetComponentsInChildren<AudioSource>()[0].clip = Resources.Load<AudioClip>("Sounds/bensound-moose");
                break;
            case 2:
                mainCamera.GetComponentsInChildren<AudioSource>()[0].clip = Resources.Load<AudioClip>("Sounds/waterflame-jumper");
                break;
            case 3:
                mainCamera.GetComponentsInChildren<AudioSource>()[0].clip = Resources.Load<AudioClip>("Sounds/at-dooms-gate");
                break;
            default:
                break;
        }
        mainCamera.GetComponentsInChildren<AudioSource>()[0].Play();
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void createRow (int zPos, string rowInfo) {
        switch (rowInfo[0]) {
            case 'c':
                createTile(zPos, 0, rowInfo[0] - 'a');
                break;
            case 'd':
                createTile(zPos, 0, rowInfo[0] - 'a');
                break;
            default:
                for (int i = 0; i < 5; i++) {
                    createTile(zPos, i, rowInfo[i] - 'a');
                }
                break;
        }
        for (int i = 5; i < Mathf.Min(10, rowInfo.Length); i++) {
            switch (rowInfo[i]) {
                case '.':
                    createCollectible(zPos, i - 5, 0);
                    break;
            }
        }
    }

    private void createTile (int zPos, int xPos, int tileType) {
        if (tileType >= 0 && tileType < tiles.Length)
            Instantiate(tiles[tileType], new Vector3(xPos, 0, zPos), Quaternion.identity, destinationObject.transform);
    }

    private void createCollectible (int zPos, int xPos, int collectibleType) {
        GameObject g;
        if (collectibleType >= 0 && collectibleType < tiles.Length) {
            g = Instantiate(collectibles[collectibleType], destinationObject.transform);
            g.transform.position += new Vector3(xPos, 0, zPos);
        }
    }
}
