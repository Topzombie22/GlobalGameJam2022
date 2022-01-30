using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    private bool move = true;
    [SerializeField]
    private bool UpNDown;
    public float xBoundsleft;
    public float xBoundsright;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (UpNDown == false)
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
        else if (UpNDown == true)
        {
            if (move == true)
            {
                transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;

                if (transform.position.y < xBoundsleft || transform.position.y > xBoundsright)
                {
                    speed = speed * -1;
                }
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
