using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    [SerializeField]
    private float rayCastCheck;
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
                RaycastHit2D hitLeftDown = (Physics2D.Raycast(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x + 0.25f, 0, 0), Vector2.down, rayCastCheck));
                if (hitLeftDown.collider == null)
                {
                    canRaycast = false;
                    turningAround = true;
                }
                else
                {
                    //     Debug.DrawRay(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x + 0.25f, 0, 0), Vector2.down, Color.blue, 1.0f);
                }

                RaycastHit2D hitLeft = (Physics2D.Raycast(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x + 0.25f, 0, 0), Vector2.left, rayCastCheck));
                if (hitLeft.collider != null && hitLeft.collider.tag != "Player")
                {
                    canRaycast = false;
                    turningAround = true;
                }
                else
                {
                    //         Debug.DrawRay(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x + 0.25f, 0, 0), Vector2.left, Color.blue, 1.0f);
                }
            }

            if (movingLeft == false)
            {
                RaycastHit2D hitRightDown = (Physics2D.Raycast(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x - 1.25f, 0, 0), Vector2.down, rayCastCheck));
                if (hitRightDown.collider == null)
                {
                    turningAround = true;
                    canRaycast = false;
                }
                else
                {
                    //      Debug.DrawRay(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x - 1.25f, 0, 0), Vector2.down, Color.blue, 1.0f);
                }

                RaycastHit2D hitRight = (Physics2D.Raycast(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x - 1.25f, 0, 0), Vector2.right, rayCastCheck));
                if (hitRight.collider != null && hitRight.collider.tag != "Player")
                {
                    canRaycast = false;
                    turningAround = true;
                }
                else
                {
                    //       Debug.DrawRay(this.gameObject.transform.position - new Vector3(pSprite.bounds.extents.x - 1.25f, 0, 0), Vector2.right, Color.blue, 1.0f);
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
