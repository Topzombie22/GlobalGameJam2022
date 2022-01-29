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

    private bool groundedSound;

    public TextBoxController text;
    public GameObject textBox;

    [SerializeField]
    private Animator anim;

    private Vector3 enemyPos;

    public AudioManagerPlayer _audio;
    public GameObject audioManager;
    [SerializeField]
    private bool isGrounded;

    public bool justTookDamage;

    private bool footStepsPlaying;


    void Start()
    {
        text = textBox.GetComponent<TextBoxController>();
        _audio = audioManager.GetComponent<AudioManagerPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        sanitySlider.value = Mathf.Lerp(sanitySlider.value, sanity, 1f * Time.deltaTime);

        Move();
    }

    private void FixedUpdate()
    {

    }

    private void Move()
    {
        input = (Input.GetAxis("Horizontal"));
        anim.SetFloat("Direction", input);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("Velocity", rb.velocity.y);
        anim.SetBool("IsDashing", isDashing);

        if (input < -0.01)
        {
            anim.SetFloat("Speed", input * -1);
            pSprite.flipX = true;
        }
        else if (input > 0.01)
        {
            anim.SetFloat("Speed", input * 1);
            pSprite.flipX = false;
        }

        if (isDashing == false && canMove == true)
        {
            transform.position += new Vector3(input, 0, 0) * Time.deltaTime * speed;
        }

        if (canRaycast == true)
        {
            if (Physics2D.Raycast(player.position - new Vector3(0, pSprite.bounds.extents.y + 0.01f, 0), Vector2.down, rayCastDist, ~4))
            {
                canJump = true;
                isGrounded = true;
                safePos = this.gameObject.transform.position;
            }
            else
            {
                groundedSound = false;
                isGrounded = false;
            }
        }

        if (groundedSound == false)
        {
            _audio.PlayLanding();
            groundedSound = true;
        }
        
        if (canJump == true)
        {
            Vector3 posFix = safePos;
        }

        if (canJump == true && Input.GetButtonDown("Jump"))
        {
            if (isDashing == false)
            {
                anim.SetTrigger("Jump");
                _audio.PlayJump();
                rb.velocity = (new Vector2(rb.velocity.x, 0));
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                canJump = false;
                canRaycast = false;
                StartCoroutine(RaycastTimer());
            }
        }

        if (canDash == true && Input.GetButtonDown("Fire2") && input != 0)
        {
            anim.SetTrigger("Dash");
            canDash = false;
            _audio.PlayDash();
            StartCoroutine(MoveTimer());
            StartCoroutine(DashTimer());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHead" && player.transform.position.y > collision.transform.position.y)
        {
            Debug.Log("Collided");
            collision.gameObject.transform.parent.localScale = new Vector3(collision.gameObject.transform.parent.localScale.x, 0.2f, 1f);
            collision.gameObject.GetComponentInParent<AIScript>().enabled = false;
            collision.gameObject.GetComponentInParent<Rigidbody2D>().simulated = false;
            collision.gameObject.transform.parent.tag = "Untagged";
            Destroy(collision.transform.parent.gameObject, 2f);
            canJump = true;
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = (new Vector2(rb.velocity.x, 0));
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            }
            else
            {

            }
        }

        if (collision.gameObject.name == "Transition")
        {

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(InvulnTimer());
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            enemyPos = collision.gameObject.transform.position;
            Debug.Log("Bang");
            text.textBox = Random.Range(4, 6);
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        justTookDamage = true;
        canMove = false;
        sanity = sanity - 1;
        if (enemyPos.x - gameObject.transform.position.x < 0)
        {
            rb.velocity = (new Vector2(0, 0));
            rb.AddForce(new Vector2(1 * jumpHeight, 6f), ForceMode2D.Impulse);
        }
        else if (enemyPos.x - gameObject.transform.position.x > 0)
        {
            rb.velocity = (new Vector2(0, 0));
            rb.AddForce(new Vector2(-1 * jumpHeight, 6f), ForceMode2D.Impulse);
        }
        _audio.PlayTakeDamage();
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    IEnumerator InvulnTimer()
    {
        yield return new WaitForSeconds(2.5f);
        justTookDamage = false;
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
        if (canMove == false)
        {
            isDashing = false;
            yield break;   
        }
        rb.velocity = new Vector2(0f, 0f);
        isDashing = false;
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(1.5f);
        canDash = true;
    }
}
