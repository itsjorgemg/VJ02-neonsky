using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public float speedForward = 5;
    [SerializeField] private float speedSide = 6;
    private float ghostFadeDuration = 0.5f;
    private float ghostActiveDuration = 1.0f;
    private float ghostAlpha = 0.5f;
    
    private IEnumerator fadeCoroutine;
    private bool ghost = false;
    private Color iniColor;
    private Color ghostColor;
    private float iniGhostTime;

    // Start is called before the first frame update
    void Start()
    {

        iniColor = gameObject.GetComponent<MeshRenderer>().material.color;
        ghostColor = new Color(iniColor.r, iniColor.g, iniColor.b, ghostAlpha);
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

        if (ghost && (Time.time - iniGhostTime) >= ghostActiveDuration) SetGhost(false);
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
        iniGhostTime = Time.time;
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = Fade(b);
        StartCoroutine(fadeCoroutine);
    }

    IEnumerator Fade(bool b)
    {
        Color startColor = b ? iniColor : ghostColor;
        Color endColor = b ? ghostColor : iniColor;
        for (var t = 0f; t < ghostFadeDuration; t += Time.deltaTime) {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(startColor, endColor, t / ghostFadeDuration);
            yield return null;
        }
    }
}
