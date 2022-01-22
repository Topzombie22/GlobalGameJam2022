using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private SpriteRenderer pSprite;
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
    private bool canRaycast;
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private float input;
    [SerializeField]
    private Vector2 movement;
    [SerializeField]
    private bool canDash;
    [SerializeField]
    private float dashDist;
    [SerializeField]
    private bool isDashing;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {

    }

    private void Move()
    {
        input = (Input.GetAxis("Horizontal"));

        if (isDashing == false)
        {
            transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;
        }

        if (canRaycast == true)
        {
            if (Physics2D.Raycast(player.position - new Vector3(0, pSprite.bounds.extents.y + 0.01f, 0), Vector2.down, rayCastDist))
            {
                canJump = true;
            }
        }

        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (isDashing == false)
            {
                rb.velocity = (new Vector2(rb.velocity.x, 0));
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                canJump = false;
                canRaycast = false;
                StartCoroutine(RaycastTimer());
            }
        }

        if (canDash == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(MoveTimer());
        }
    }

    IEnumerator RaycastTimer()
    {
        yield return new WaitForSeconds(0.1f);
        canRaycast = true;
    }

    IEnumerator MoveTimer()
    {
        isDashing = true;
        canDash = false;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        if (input > 0.01)
        {
            rb.AddForce(new Vector2(dashDist * 1, 0f), ForceMode2D.Impulse);
        }
        else if (input < -0.01)
        {
            rb.AddForce(new Vector2(dashDist * - 1, 0f), ForceMode2D.Impulse);
        }
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.25f);
        rb.gravityScale = gravity;
        rb.velocity = new Vector2(0f, 0f);
        isDashing = false;
        canDash = true;
    }

}
