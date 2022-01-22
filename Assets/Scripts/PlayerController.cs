using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController player;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float gravity;
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
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0f);
        Vector2 movement = input * speed;
        player.Move(movement * Time.deltaTime);
    }

    private void Jump()
    {
        if (Physics2D.Raycast(player.transform.position, Vector2.down, rayCastDist))
        {
            isGrounded = true;
            canJump = true;
        }

        if (isGrounded == false)
        {
            player.Move(new Vector2(player.transform.position.x, gravity * Time.deltaTime));
        }
        else
        {

        }

    }
}
