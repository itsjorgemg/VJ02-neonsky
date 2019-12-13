using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [SerializeField] private float bouncingTime = 1.0f;
    [SerializeField] private float rotatingTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sinus = 0.5f + Mathf.Sin(Time.time * (360.0f / bouncingTime) * Mathf.Deg2Rad) * 0.1f;
        transform.position = new Vector3(transform.position.x, sinus, transform.position.z);
        transform.Rotate(0, Time.deltaTime * 360 / rotatingTime, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerBehavior>().AddCoin();
            Destroy(gameObject);
        }
    }
}
