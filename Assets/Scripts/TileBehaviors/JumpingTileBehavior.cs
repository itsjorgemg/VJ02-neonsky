using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingTileBehavior : MonoBehaviour
{

    [SerializeField] private float jumpForce = 7.65f;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float animationScaleY = 1.5f;
    private bool animate = false;
    private float startTime;
    private Vector3 iniScale;

    // Start is called before the first frame update
    void Start()
    {
        iniScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (animate) {
            // (sin(x - π/2 + (1 - 0.1)) + 1) * 0.5 * 1.5
            float sinus = (Mathf.Sin(((Time.time - startTime) * (360.0f / animationDuration) - 90) * Mathf.Deg2Rad + (1 - iniScale.y)) + 1) * 0.5f * animationScaleY;
            transform.localScale = new Vector3(iniScale.x, sinus, iniScale.z);
            if (sinus <= iniScale.y) {
                animate = false;
                transform.localScale = iniScale;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !animate) {
            GetComponent<AudioSource>().Play();
            other.attachedRigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            animate = true;
            startTime = Time.time;
            other.gameObject.GetComponent<PlayerBehavior>().airborne = true;
            other.GetComponentsInChildren<ParticleSystem>()[0].Pause(true);
        }
    }
}
