using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelCreator : MonoBehaviour
{

    [SerializeField] private FileInfo sourceFile = new FileInfo("Assets/Levels/Level1.txt");
    [SerializeField] private GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader = sourceFile.OpenText();
        string text = reader.ReadLine();

        for (int i = 0; text != null; ++i) {
            createRow(i, text);
            text = reader.ReadLine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createRow (int zPos, string rowInfo) {
        switch (rowInfo[0]) {
            case '3':
                createTile(zPos, 0, rowInfo[0] - '0');
                break;
            case '4':
                createTile(zPos, 0, rowInfo[0] - '0');
                break;
            default:
                for (int j = 0; j < 5; ++j) {
                    createTile(zPos, j, rowInfo[j] - '0');
                }
                break;
        }
    }

    private void createTile (int zPos, int xPos, int tileType) {
        /*switch (tileType) {
            case 1:
                Instantiate((GameObject) Resources.Load("Prefabs/BasicTile"), new Vector3(xPos, 0, zPos), Quaternion.identity);
                break;
            case 2:
                Instantiate((GameObject) Resources.Load("Prefabs/ObstacleWall"), new Vector3(xPos, 0, zPos), Quaternion.identity);
                break;
            case 3:
                Instantiate((GameObject) Resources.Load("Prefabs/MovingTilePack"), new Vector3(xPos, 0, zPos), Quaternion.identity);
                break;
            case 4:
                Instantiate((GameObject) Resources.Load("Prefabs/BlinkingTilePack"), new Vector3(xPos, 0, zPos), Quaternion.identity);
                break;
            default:
                break;
        }*/
        if (tileType > 0) Instantiate(tiles[tileType - 1], new Vector3(xPos, 0, zPos), Quaternion.identity);
    }
}
