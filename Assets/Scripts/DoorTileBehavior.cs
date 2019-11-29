using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTileBehavior : MonoBehaviour
{
    private Transform door;

    private Color iniColor;
    private Color transparentColor;
    private float fadeOutDuration = 0.75f;
    private IEnumerator fadeOutCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentsInParent<Transform>()[1].GetComponentsInChildren<Transform>()[1];

        iniColor = door.GetComponent<MeshRenderer>().material.color;
        transparentColor = new Color(iniColor.r, iniColor.g, iniColor.b, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (fadeOutCoroutine == null && collision.gameObject.CompareTag("Player")) {
            fadeOutCoroutine = FadeOut();
            StartCoroutine(fadeOutCoroutine);
        }
    }

    IEnumerator FadeOut()
    {
        Vector3 scale = door.localScale;
        for (var t = 0f; t < fadeOutDuration; t += Time.deltaTime) {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(iniColor, transparentColor, t / fadeOutDuration);

            door.localScale = Vector3.Lerp(scale, new Vector3(scale.x, 0.0f, scale.z), t / fadeOutDuration);

            yield return null;
        }
        door.gameObject.SetActive(false);
    }
}
