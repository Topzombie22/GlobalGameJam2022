using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    public GameObject player;
    public GameObject stick;
    public Transform transitionZone;
    public Transform zoneTwoSpawn;

    public GameObject moreTop;
    public GameObject top;
    public GameObject mid;
    public GameObject bot;

    public GameObject deathScreen;
    public Image fadeAble;
    public RawImage backGround;
    public Text sanityText;
    public Image sanitySprite;
    public Image sanityBar;

    public GameObject audioManager;
    public AudioManagerPlayer _audio;

    [SerializeField]
    private int startVal;
    [SerializeField]
    private int endVal;

    private bool moveStick;

    private bool falling;

    private bool volumeLerp;

    // Start is called before the first frame update
    void Start()
    {
        _audio = audioManager.GetComponent<AudioManagerPlayer>();   
    }

    // Update is called once per frame
    void Update()
    {
        MoveStick();
        Falling();
        VolumeLerp();
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Hello");
            StartCoroutine(Transition());
        }
    }

    public void MoveStick()
    {
        if (moveStick == true)
        {
            stick.transform.position = new Vector2(stick.transform.position.x, stick.transform.position.y - 12f * Time.deltaTime);
        }
        else
        {
            stick.transform.position = new Vector3(stick.transform.position.x, stick.transform.position.y);
        }
    }

    public void VolumeLerp()
    {
        if (volumeLerp == true)
        {
            _audio.aboveGroundTheme.volume = Mathf.Lerp(_audio.aboveGroundTheme.volume, 0f, 1f * Time.deltaTime);
            _audio.underGroundTheme.volume = Mathf.Lerp(_audio.underGroundTheme.volume, 0.5f, 1f * Time.deltaTime);
        }
    }
    
    public void Falling()
    {
        if (falling == true)
        {
            moreTop.transform.position = new Vector2(moreTop.transform.position.x, moreTop.transform.position.y + 15f * Time.deltaTime);
            top.transform.position = new Vector2(top.transform.position.x, top.transform.position.y + 15f * Time.deltaTime);
            mid.transform.position = new Vector2(mid.transform.position.x, mid.transform.position.y + 15f * Time.deltaTime);
            bot.transform.position = new Vector2(bot.transform.position.x, bot.transform.position.y + 15f * Time.deltaTime);

            if(bot.transform.position.y >= endVal)
            {
                bot.transform.position = new Vector2(bot.transform.position.x, startVal);
            }
            if (mid.transform.position.y >= endVal)
            {
                mid.transform.position = new Vector2(mid.transform.position.x, startVal);
            }
            if (top.transform.position.y >= endVal)
            {
                top.transform.position = new Vector2(top.transform.position.x, startVal);
            }
            if (moreTop.transform.position.y >= endVal)
            {
                moreTop.transform.position = new Vector2(moreTop.transform.position.x, startVal);
            }
        }
    }

    IEnumerator Transition()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Animator>().SetFloat("Direction", 0f); // needs to be changed later
        yield return new WaitForSeconds(0.5f);
        moveStick = true;
        player.GetComponent<Animator>().SetBool("IsGrounded", false);
        player.GetComponent<Animator>().SetFloat("Velocity", -0.1f);
        deathScreen.SetActive(true);
        sanityBar.CrossFadeAlpha(1f, 1f, false);
        sanitySprite.CrossFadeAlpha(1f, 1f, false);
        sanityText.CrossFadeAlpha(1f, 1f, false);
        backGround.CrossFadeAlpha(255f, 1f, false);
        yield return new WaitForSeconds(1f);
        moveStick = false;
        player.transform.position = new Vector2(transitionZone.position.x, transitionZone.position.y - 5);
        player.GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(1f);
        falling = true;
        volumeLerp = true;
        _audio.PlayFall();
        backGround.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1f);
        deathScreen.SetActive(false);
        yield return new WaitForSeconds(2f);
        _audio.aboveGroundTheme.mute = true;
        deathScreen.SetActive(true);
        backGround.CrossFadeAlpha(255f, 1f, false);
        yield return new WaitForSeconds(1.5f);
        player.transform.position = new Vector2(zoneTwoSpawn.position.x, zoneTwoSpawn.position.y);
        yield return new WaitForSeconds(1f);
        backGround.CrossFadeAlpha(1f, 1f, false);
        player.GetComponent<Rigidbody2D>().simulated = true;
        yield return new WaitForSeconds(1f);
        deathScreen.SetActive(false);
        player.GetComponent<PlayerController>().enabled = true;
    }

}
