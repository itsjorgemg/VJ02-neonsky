using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTileBehavior : MonoBehaviour
{

    private Vector3 start;
    private Vector3 end;
    private float distance = 4.0f;
    private float startTime;
    [SerializeField] private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = start + new Vector3(distance, 0, 0);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float fraction = (Time.time - startTime) * speed / distance;
        transform.position = Vector3.Lerp(start, end, fraction);
        if (transform.position == end) {
            Vector3 aux = start;
            start = end;
            end = aux;
            startTime = Time.time;
        }
    }
}
