using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponentsInChildren<AudioSource>()[0].Play();
        if (collision.gameObject.CompareTag("Player")) collision.gameObject.GetComponent<PlayerBehavior>().SetGhost(true);
    }
}
