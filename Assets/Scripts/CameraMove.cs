using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject objectToFollow;
    public Vector3 offset;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(angle, 0.0f, 0.0f, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.transform.position + offset;
    }
}
