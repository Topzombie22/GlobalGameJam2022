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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Touched");
            collision.gameObject.GetComponent<PlayerController>().enabled = false;
            deathScreen.SetActive(true);
            sanityBar.CrossFadeAlpha(0f, 2f, false);
            sanitySprite.CrossFadeAlpha(0f, 2f, false);
            sanityText.CrossFadeAlpha(0f, 2f, false);
            backGround.CrossFadeAlpha(255f, 2f, false);
            StartCoroutine(TextTimer());
        }
    }

    IEnumerator TextTimer()
    {
        yield return new WaitForSeconds(2f);
        _text.CrossFadeAlpha(255f, 2f, false);
    }

}
