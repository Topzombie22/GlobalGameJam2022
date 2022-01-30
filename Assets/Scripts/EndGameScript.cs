using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public GameObject deathScreen;
    public RawImage backGround;
    public Text sanityText;
    public Image sanitySprite;
    public Image sanityBar;
    public GameObject cam;

    private bool moving;

    public GameObject player;
    public GameObject spike;

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
        if (moving == true)
        {
            spike.transform.position += new Vector3(0, -1, 0) * Time.deltaTime * 20f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Animator>().SetFloat("Direction", 0f);
        yield return new WaitForSeconds(1f);
        moving = true;
        yield return new WaitForSeconds(0.25f);
        moving = false;
        deathScreen.SetActive(true);
        backGround.CrossFadeColor(Color.red, 1f, false, false);
        yield return new WaitForSeconds(1f);
        backGround.CrossFadeColor(Color.black, 1f, false, false);
    }

}
