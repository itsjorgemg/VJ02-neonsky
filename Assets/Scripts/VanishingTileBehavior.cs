using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingTileBehavior : MonoBehaviour
{
    private float fadeOutDuration = 0.5f;

    IEnumerator fadeOutCoroutine;
    private Color iniColor;
    private Color transparentColor;

    // Start is called before the first frame update
    void Start()
    {
        iniColor = GetComponentInChildren<MeshRenderer>().material.color;
        transparentColor = new Color(iniColor.r, iniColor.g, iniColor.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && fadeOutCoroutine == null) {
            GetComponent<AudioSource>().Play();
            fadeOutCoroutine = FadeOut();
            StartCoroutine(fadeOutCoroutine);
        }
    }

    IEnumerator FadeOut()
    {
        for (var t = 0f; t < fadeOutDuration; t += Time.deltaTime) {
            foreach (var renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
                renderer.material.color = Color.Lerp(iniColor, transparentColor, t / fadeOutDuration);
            
            yield return null;
        }
        foreach (var collider in gameObject.GetComponentsInChildren<BoxCollider>())
            collider.enabled = false;
    }
}
