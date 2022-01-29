using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private bool zone1;
    [SerializeField]
    public float sanity = 4;
    [SerializeField]
    public Slider sanitySlider;
    [SerializeField]
    public Vector3 safePos;

    public AudioManagerPlayer _audio;
    public GameObject audioManager;
    [SerializeField]
    private bool isGrounded;

    private bool footStepsPlaying;


    void Start()
    {
        _audio = audioManager.GetComponent<AudioManagerPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sanitySlider.value = Mathf.Lerp(sanitySlider.value, sanity, 6.0f * Time.deltaTime);

        Move();
    }

    private void FixedUpdate()
    {

    }

    private void Move()
    {
        input = (Input.GetAxis("Horizontal"));

        if (isGrounded == true && input != 0)
        {
            _audio.PlayFootsteps();
        }
        if (isGrounded == false && input == 0)
        {
            _audio.StopFootsteps();
        }

        if (isDashing == false)
        {
            transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;
        }

        if (canRaycast == true)
        {
            if (Physics2D.Raycast(player.position - new Vector3(0, pSprite.bounds.extents.y + 0.01f, 0), Vector2.down, rayCastDist, ~3))
            {
                canJump = true;
                isGrounded = true;
                safePos = this.gameObject.transform.position;
            }
            else
            {
                isGrounded = false;
            }
        }

        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            if (isDashing == false)
            {
                _audio.PlayJump();
                rb.velocity = (new Vector2(rb.velocity.x, 0));
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                canJump = false;
                canRaycast = false;
                StartCoroutine(RaycastTimer());
            }
        }

        if (canDash == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            canDash = false;
            _audio.PlayDash();
            StartCoroutine(MoveTimer());
            StartCoroutine(DashTimer());
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
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(1.5f);
        canDash = true;
    }

}
