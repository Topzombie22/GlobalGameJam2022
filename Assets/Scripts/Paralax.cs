using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float length;
    private float startPos;
    private float startPosY;

    public GameObject cam;
    public float parallaxEffect;
    public float parallaxEffect2;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);
        float dist2 = (cam.transform.position.y * parallaxEffect2);

        transform.position = new Vector3(startPos + dist, startPosY + dist2, transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
