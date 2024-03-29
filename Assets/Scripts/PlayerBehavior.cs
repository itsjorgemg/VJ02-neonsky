﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public float speedForward = 5;
    private float currentAcceleration = 0;
    [SerializeField] private float maxAccSide = 6;
    [SerializeField] private float accelerationRate = 20;
    private float ghostFadeDuration = 0.5f;
    private float ghostActiveDuration = 1.0f;
    private float ghostAlpha = 0.5f;
    
    private IEnumerator fadeCoroutine;
    private bool ghost = false;
    private Color iniColor;
    private Color ghostColor;
    private float iniGhostTime;

    private Vector3 iniScale;
    private Vector3 moveSideScale;

    public bool paused = false;

    private ParticleSystem trailParticles;
    private ParticleSystem landingParticles;
    private ParticleSystem explosionParticles;

    public bool airborne = false;

    public int coins { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {

        iniColor = gameObject.GetComponent<MeshRenderer>().material.color;
        ghostColor = new Color(iniColor.r, iniColor.g, iniColor.b, ghostAlpha);
        iniScale = transform.localScale;
        moveSideScale = iniScale - new Vector3(iniScale.x / 8, 0, 0);

        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        trailParticles = particles[0];
        landingParticles = particles[1];
        explosionParticles = particles[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.instance != null && !UIController.instance.IsAnyPanelOpened() && Input.GetKeyDown(KeyCode.Escape)) {
            UIController.instance.SetPauseMenuPanel(!UIController.instance.GetPauseMenuPanel());
        }

        //Move sidewise
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (Mathf.Abs(currentAcceleration) < 0.1) currentAcceleration = 0;
            if (currentAcceleration < 0) currentAcceleration += Time.deltaTime * accelerationRate / 1.25f;
            else if (currentAcceleration > 0) currentAcceleration -= Time.deltaTime * accelerationRate / 1.25f;
            MoveSideScale(false);
        }
        
        if (paused) return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Mathf.Abs(currentAcceleration) < maxAccSide || currentAcceleration > 0) currentAcceleration -= Time.deltaTime * accelerationRate;
            MoveSideScale(true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Mathf.Abs(currentAcceleration) < maxAccSide || currentAcceleration < 0) currentAcceleration += Time.deltaTime * accelerationRate;
            MoveSideScale(true);
        }
        transform.Translate(currentAcceleration * Time.deltaTime, 0.0f, 0.0f);

        //Move forward
        transform.Translate(0.0f, 0.0f, speedForward * Time.deltaTime);
        if (transform.position.y < -5) GameOver();

        if (ghost && (Time.time - iniGhostTime) >= ghostActiveDuration) SetGhost(false);
    }

    public void ObstacleHit ()
    {
        if (!ghost) GameOver();
    }

    public void GameOver()
    {
        paused = true;
        explosionParticles.Play(true);
        trailParticles.Stop(true);
        if (!UIController.instance.IsAnyPanelOpened())
            UIController.instance.SetGameOverPanel(true);
        fadeCoroutine = Fade(true);
        StartCoroutine(fadeCoroutine);
    }

    public void EndGame()
    {
        paused = true;
        trailParticles.Stop(true);
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

    public void AddCoin() {
        coins++;
    }

    public IEnumerator Fade(bool b)
    {
        Color startColor = b ? iniColor : ghostColor;
        Color endColor = b ? ghostColor : iniColor;
        for (var t = 0f; t < ghostFadeDuration; t += Time.deltaTime) {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(startColor, endColor, t / ghostFadeDuration);
            yield return null;
        }
    }

    private void MoveSideScale (bool b)
    {
        if (b) transform.localScale = Vector3.Lerp(transform.localScale, moveSideScale, Time.deltaTime * maxAccSide);
        else transform.localScale = Vector3.Lerp(transform.localScale, iniScale, Time.deltaTime * maxAccSide);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            if (airborne) {
                trailParticles.Play(true);
                landingParticles.Play(true);
                airborne = false;
            }
            var main = trailParticles.main;
            main.startColor = collision.gameObject.GetComponent<MeshRenderer>().material.color;
        }
            
    }
}
