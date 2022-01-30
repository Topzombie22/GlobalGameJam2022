using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    private bool move = true;
    public float xBoundsleft;
    public float xBoundsright;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (move == true)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;

            if (transform.position.x < xBoundsleft || transform.position.x > xBoundsright)
            {
                speed = speed * -1;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ping");
        collision.gameObject.transform.SetParent(gameObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Pong");
        collision.gameObject.transform.SetParent(null);
    }
}
