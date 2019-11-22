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
        if (holdingObject != null) transform.Translate(0.0f, 0.0f, (PlayerBehavior.speedForward) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) holdingObject = collision.gameObject;
        else if (collision.gameObject.CompareTag("Ground")) {
            holdingObject = null;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) holdingObject = null;
    }
}
