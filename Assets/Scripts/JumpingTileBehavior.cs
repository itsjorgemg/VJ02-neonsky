using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingTileBehavior : MonoBehaviour
{

    [SerializeField] private float jumpForce = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.attachedRigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
}
