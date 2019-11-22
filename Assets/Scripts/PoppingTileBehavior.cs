using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingTileBehavior : MonoBehaviour
{

    private GameObject player;
    private Color iniColor;
    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        iniColor = GetColor();
        currentColor = new Color(iniColor.r, iniColor.g, iniColor.b, 0.0f);
        SetColor(currentColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 4)
            SetColor(Color.Lerp (GetColor(), iniColor, 3 * Time.deltaTime));
    }

    private Color GetColor()
    {
        return gameObject.GetComponent<MeshRenderer>().material.color;
    }

    private void SetColor(Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
