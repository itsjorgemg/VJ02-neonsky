using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTileGlow : MonoBehaviour
{
    [SerializeField] private float iterationTime = 1.0f;
    private Color initialColor;
    private Material tileMaterial;

    // Start is called before the first frame update
    void Start()
    {
        tileMaterial = GetComponent<Renderer>().material;
        initialColor = tileMaterial.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        float sinus = Mathf.Sin(Time.time * (360.0f / iterationTime) * Mathf.Deg2Rad) + 2.0f;
        tileMaterial.SetColor("_EmissionColor", initialColor * sinus);
    }
}
