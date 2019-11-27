﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingTileBehavior : MonoBehaviour
{

    private float fadeInDuration = 0.8f;
    private float fadeInDistance = 4.0f;

    IEnumerator fadeInCoroutine;
    private GameObject player;
    private Color iniColor;
    private Color transparentColor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        iniColor = gameObject.GetComponent<MeshRenderer>().material.color;
        transparentColor = new Color(iniColor.r, iniColor.g, iniColor.b, 0.0f);
        gameObject.GetComponent<MeshRenderer>().material.color = transparentColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeInCoroutine == null && Vector3.Distance(player.transform.position, transform.position) <= fadeInDistance) {
            fadeInCoroutine = FadeIn();
            StartCoroutine(fadeInCoroutine);
        }
    }

    IEnumerator FadeIn()
    {
        for (var t = 0f; t < fadeInDuration; t += Time.deltaTime) {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(transparentColor, iniColor, t / fadeInDuration);
            yield return null;
        }
    }
}
