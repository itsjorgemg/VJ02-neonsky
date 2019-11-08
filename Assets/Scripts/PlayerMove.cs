using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float speedForward;
    public float speedSide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move sidewise
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(-speedSide * Time.deltaTime, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(speedSide * Time.deltaTime, 0.0f, 0.0f);
        //Move forward
        transform.Translate(0.0f, 0.0f, speedForward * Time.deltaTime);
    }
}
