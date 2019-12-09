using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingTileBehavior : MonoBehaviour
{

    [SerializeField] private float period = 3.0f;
    private float startTime;
    private Transform[] childTiles;

    private bool blink = true;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        childTiles = GetComponentsInChildren<Transform>();
        for (int i = 1; i < childTiles.Length; i++) {
            childTiles[i].gameObject.SetActive(i % 2 != 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= period) {
            if (blink)
                for (int i = 1; i < childTiles.Length; i++)
                    childTiles[i].gameObject.SetActive(!childTiles[i].gameObject.activeSelf);
            startTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            blink = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            blink = true;
        }
    }
}
