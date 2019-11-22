using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public static float speedForward = 5;
    public float speedSide = 6;
    private bool ghost = false;
    private Color iniColor;
    private Color ghostColor;
    private Color targetColor;
    private float iniGhostTime;

    // Start is called before the first frame update
    void Start()
    {

        iniColor = GetColor();
        ghostColor = new Color(iniColor.r, iniColor.g, iniColor.b, 0.5f);
        targetColor = iniColor;
    }

    // Update is called once per frame
    void Update()
    {
        //Move sidewise
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(-speedSide * Time.deltaTime, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(speedSide * Time.deltaTime, 0.0f, 0.0f);
        //Move forward
        transform.Translate(0.0f, 0.0f, speedForward * Time.deltaTime);

        if (ghost && (Time.time - iniGhostTime) >= 1) SetGhost(false);
        SetColor(Color.Lerp(GetColor(), targetColor, 1 * Time.deltaTime));
    }

    public void ObstacleHit ()
    {
        if (!ghost) GameOver();
    }

    public void GameOver()
    {
        transform.position = new Vector3(2, 0.3f, 0);
    }

    public void SetGhost(bool b)
    {
        ghost = b;
        targetColor = b ? ghostColor : iniColor;
        iniGhostTime = Time.time;
    }

    private Color GetColor()
    {
        return gameObject.GetComponent<MeshRenderer>().material.color;
    }

    private void SetColor(Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
