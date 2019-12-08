using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject objectToFollow;
    public Vector3 offset = new Vector3(0.0f, 4f, -3.25f);
    public float angle = 30;
    [SerializeField] private float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentsInChildren<AudioSource>()[0].Play();
        transform.position = objectToFollow.transform.position + offset;
        transform.Rotate(angle, 0.0f, 0.0f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, objectToFollow.transform.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position,
                                        objectToFollow.transform.position - new Vector3(0, objectToFollow.transform.position.y, 0) + offset, 
                                        Time.deltaTime * speed);
    }
}
