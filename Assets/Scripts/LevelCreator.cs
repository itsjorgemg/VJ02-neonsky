using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelCreator : MonoBehaviour
{

    [SerializeField] private FileInfo sourceFile = new FileInfo("Assets/Levels/Level1.txt");
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private GameObject destinationObject;

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
                for (int j = 0; j < 5; ++j) {
                    createTile(zPos, j, rowInfo[j] - 'a');
                }
                break;
        }
    }

    private void createTile (int zPos, int xPos, int tileType) {
        if (tileType >= 0 && tileType < tiles.Length)
            Instantiate(tiles[tileType], new Vector3(xPos, 0, zPos), Quaternion.identity, destinationObject.transform);
    }
}
