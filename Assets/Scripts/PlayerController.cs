using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float rayCastDist;
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private Vector2 input;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0f);
        Vector2 movement = (input) * speed;
        rb.velocity = movement;
    }
}
