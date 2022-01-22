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
        transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;
        
        if (canRaycast == true)
        {
            if (Physics2D.Raycast(player.position - new Vector3(0, pSprite.bounds.extents.y + 0.01f, 0), Vector2.down, rayCastDist))
            {
                canJump = true;
            }
        }

        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = (new Vector2(rb.velocity.x, 0));
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            canJump = false;
            canRaycast = false;
            StartCoroutine(RaycastTimer());
        }

        //if (canDash == true && Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    if (rb.velocity.x > 0.01)
        //    {
        //      //  Vector3.Lerp(new Vector3(player.transform.position.x))
        //    }

        //    if (rb.velocity.x < -0.01)
        //    {

        //    }
        //}
    }

    IEnumerator RaycastTimer()
    {
        yield return new WaitForSeconds(0.1f);
        canRaycast = true;
    }

}
