using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GetComponentInParent<Transform>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
