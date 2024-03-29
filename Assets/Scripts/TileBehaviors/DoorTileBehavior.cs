﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTileBehavior : MonoBehaviour
{
    private Transform door;
    private Transform indicator;

    private float fadeOutDuration = 0.75f;
    private IEnumerator fadeOutCoroutine;
    private IEnumerator indicateCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentsInParent<Transform>()[1].GetComponentsInChildren<Transform>()[1];
        indicator = GetComponentsInChildren<Transform>()[2];
        indicator.gameObject.GetComponentsInChildren<Transform>()[1].SetParent(indicator);
        indicator.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponentsInChildren<AudioSource>()[0].Play();
        gameObject.GetComponentsInParent<Transform>()[1].GetComponentsInChildren<AudioSource>()[0].Play();
        if (fadeOutCoroutine == null && collision.gameObject.CompareTag("Player")) {
            fadeOutCoroutine = FadeOut();
            indicateCoroutine = Indicate();
            StartCoroutine(fadeOutCoroutine);
            StartCoroutine(indicateCoroutine);
        }
    }

    IEnumerator FadeOut()
    {
        Vector3 scale = door.localScale;
        for (var t = 0f; t < fadeOutDuration; t += Time.deltaTime) {
            door.localScale = Vector3.Lerp(scale, new Vector3(scale.x, 0.0f, scale.z), t / fadeOutDuration);
            yield return null;
        }
        door.gameObject.SetActive(false);
    }

    IEnumerator Indicate()
    {
        indicator.gameObject.SetActive(true);
        Vector3 goal = door.position;
        for (var t = 0f; t < fadeOutDuration; t += Time.deltaTime)
        {
            indicator.transform.position = Vector3.Lerp(indicator.transform.position, goal, t / (fadeOutDuration * 4));
            yield return null;
        }
        indicator.gameObject.SetActive(false);
    }
}
