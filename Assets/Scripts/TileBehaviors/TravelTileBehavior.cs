using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelTileBehavior : MonoBehaviour
{

    private GameObject holdingObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (holdingObject != null) transform.Translate(0.0f, 0.0f, holdingObject.GetComponent<PlayerBehavior>().speedForward * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            holdingObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            gameObject.GetComponentsInChildren<AudioSource>()[0].Play();
            holdingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            gameObject.GetComponentsInChildren<AudioSource>()[0].Stop();
            gameObject.GetComponentsInChildren<AudioSource>()[1].Play();
            holdingObject = null;
        }
    }
}
