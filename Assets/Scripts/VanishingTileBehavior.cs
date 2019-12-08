using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingTileBehavior : MonoBehaviour
{
    private float fadeOutDuration = 0.75f;
    private bool trigger = false;

    IEnumerator fadeOutCoroutine;
    private Color iniColor;
    private Color transparentColor;
    private float timeIniVanish;

    // Start is called before the first frame update
    void Start()
    {
        iniColor = new Color(1, 1, 1, 1);
        transparentColor = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutCoroutine == null && trigger) {
            fadeOutCoroutine = FadeOut();
            StartCoroutine(fadeOutCoroutine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!trigger) {
                gameObject.GetComponentsInChildren<AudioSource>()[0].Play();
                timeIniVanish = Time.time;
                trigger = true;
                for (int i = 0; i < 5; ++i) gameObject.GetComponentsInChildren<MeshRenderer>()[i].material.color = iniColor;
            }
        }
    }

    IEnumerator FadeOut()
    {
        for (var t = 0f; t < fadeOutDuration; t += Time.deltaTime) {
            for (int i = 0; i < 5; ++i) {
                gameObject.GetComponentsInChildren<MeshRenderer>()[i].material.color = Color.Lerp(iniColor, transparentColor, t / fadeOutDuration);
                if (Time.time - timeIniVanish >= 0.75) gameObject.GetComponentsInChildren<BoxCollider>()[i+1].enabled = false;
            }
            yield return null;
        }
    }
}
