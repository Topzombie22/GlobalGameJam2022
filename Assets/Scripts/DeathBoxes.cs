using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathBoxes : MonoBehaviour
{
    [SerializeField]
    private RawImage backGround;
    [SerializeField]
    private Text _text;
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private Text sanityText;
    [SerializeField]
    private Image sanitySprite;
    [SerializeField]
    private Image sanityBar;
    private GameObject player;

    [SerializeField]
    private Transform cam;

    public AudioManagerPlayer _audio;
    public GameObject audioManager;


    // Start is called before the first frame update
    void Start()
    {
        _audio = audioManager.GetComponent<AudioManagerPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;

            if (player.GetComponent<PlayerController>().sanity <= 0)
            {
                var vcam = cam.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
                vcam.LookAt = null;
                vcam.Follow = null;
                Debug.Log("Touched2");
                collision.gameObject.GetComponent<PlayerController>().enabled = false;
                deathScreen.SetActive(true);
                sanityBar.CrossFadeAlpha(0f, 2f, false);
                sanitySprite.CrossFadeAlpha(0f, 2f, false);
                sanityText.CrossFadeAlpha(0f, 2f, false);
                backGround.CrossFadeAlpha(255f, 2f, false);
                _audio.PlayFall();
                StartCoroutine(TextTimer());
            }

            if (player.GetComponent<PlayerController>().sanity > 0)
            {
                var vcam = cam.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
                vcam.LookAt = null;
                vcam.Follow = null;
                if (player.GetComponent<PlayerController>().justTookDamage == false)
                {
                    player.GetComponent<PlayerController>().sanity -= 1f;
                }
                Debug.Log("Touched1");
                collision.gameObject.GetComponent<PlayerController>().enabled = false;
                deathScreen.SetActive(true);
                sanityBar.CrossFadeAlpha(1f, 2f, false);
                sanitySprite.CrossFadeAlpha(1f, 2f, false);
                sanityText.CrossFadeAlpha(1f, 2f, false);
                backGround.CrossFadeAlpha(255f, 2f, false);
                _audio.PlayFall();
                StartCoroutine(AliveTimer());
            }
        }
    }

    IEnumerator TextTimer()
    {
        yield return new WaitForSeconds(2f);
        _text.CrossFadeAlpha(255f, 2f, false);
    }


    IEnumerator AliveTimer()
    {
        yield return new WaitForSeconds(2f);
        player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        var vcam = cam.gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        vcam.LookAt = player.transform;
        vcam.Follow = player.transform;
        player.gameObject.GetComponent<PlayerController>().transform.position = player.gameObject.GetComponent<PlayerController>().safePos;
        sanityBar.CrossFadeAlpha(255f, 2f, false);
        sanitySprite.CrossFadeAlpha(255f, 2f, false);
        sanityText.CrossFadeAlpha(255f, 2f, false);
        backGround.CrossFadeAlpha(0f, 2f, false);
        player.gameObject.GetComponent<PlayerController>().enabled = true;
        yield return new WaitForSeconds(2f);
    }
}
