using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingTileBehavior : MonoBehaviour
{

    [SerializeField] private float jumpForce = 6;
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
        if (collision.transform.gameObject.tag == "Player") collision.rigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
}
