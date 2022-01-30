using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    [SerializeField]
    private float rayCastCheck;
    [SerializeField]
    private float rayCastCheckDown;
    [SerializeField]
    private float raycastPosx;
    [SerializeField]
    private float raycastPosy;
    [SerializeField]
    private bool movingLeft;
    [SerializeField]
    private bool turningAround;
    [SerializeField]
    private bool canMove;
    [SerializeField]
    private bool canRaycast;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private BoxCollider2D bc;

    [SerializeField]
    private bool jumping;
    [SerializeField]
    private float speed;
    [SerializeField]
    private SpriteRenderer pSprite;

    // Start is called before the first frame update
    void Start()
    {
        pSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        canRaycast = true;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastingCheck();
        Movement();
    }

    private void RaycastingCheck()
    {

        if(canRaycast == true)
        {
            if (movingLeft == true)
            {
                RaycastHit2D hitLeftDown = (Physics2D.Raycast(this.gameObject.transform.position - new Vector3(bc.bounds.extents.x + 0.5f, 0, 0), Vector2.down, rayCastCheckDown));
                if (hitLeftDown.collider == null)
                {
                    canRaycast = false;
                    turningAround = true;
                    Debug.Log("1");
                }
                else
                {
                    Debug.DrawRay(this.gameObject.transform.position - new Vector3(bc.bounds.extents.x + 0.5f, 0, 0), Vector2.down, Color.blue, 1.0f);
                }
            }

            if (movingLeft == false)
            {
                RaycastHit2D hitRightDown = (Physics2D.Raycast(this.gameObject.transform.position - new Vector3(bc.bounds.extents.x - 3f, 0, 0), Vector2.down, rayCastCheckDown));
                if (hitRightDown.collider == null)
                {
                    turningAround = true;
                    canRaycast = false;
                    Debug.Log("3");
                }
                else
                {
                    Debug.DrawRay(this.gameObject.transform.position - new Vector3(bc.bounds.extents.x - 3f, 0, 0), Vector2.down, Color.blue, 1.0f);
                }
            }
        }

        if (turningAround == true)
        {
            if (movingLeft == true)
            {
                turningAround = false;
                StartCoroutine(TurnAround());
                return;
            }
            else if (movingLeft == false)
            {
                turningAround = false;
                StartCoroutine(TurnAroundLeft());
                return;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            turningAround = true;
            canRaycast = false;
        }
    }

    private void Movement()
    {
        if (canMove == true)
        {
            if (movingLeft == true)
            {
                transform.position += new Vector3(Time.deltaTime * -speed, 0, 0);
            }
            else if (movingLeft == false)
            {
                transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
            }
        }
    }

    IEnumerator TurnAround()
    {
        canMove = false;
        float ranNum = Random.Range(0.5f, 4f);
        yield return new WaitForSeconds(ranNum);
        movingLeft = false;
        canMove = true;
        canRaycast = true;
    }

    IEnumerator TurnAroundLeft()
    {
        canMove = false;
        float ranNum = Random.Range(0.5f, 4f);
        yield return new WaitForSeconds(ranNum);
        movingLeft = true;
        canMove = true;
        canRaycast = true;
    }
}
