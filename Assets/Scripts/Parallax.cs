using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxFactor; // Adjust this value for different layers
    private float length, startPos;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxFactor));
        float distance = (cam.transform.position.x * parallaxFactor);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // Loop the background
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
