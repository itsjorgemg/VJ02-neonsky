using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelTileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player") transform.Translate(0.0f, 0.0f, PlayerMove.speedForward * Time.deltaTime);
    }*/

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player") transform.Translate(0.0f, 0.0f, (PlayerBehavior.speedForward - 0.5f) * Time.deltaTime);
    }
}
