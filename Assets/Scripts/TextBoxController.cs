using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxController : MonoBehaviour
{
    public TextMesh text;
    public int textBox;
    private string curText;
    private string textWipe = "";
    private bool skip;
    private bool talking;

    // Start is called before the first frame update
    void Start()
    {
        textBox = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tutorial")
        {
            if (collision.gameObject.name == "Tut1")
            {
                textBox = 1;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.name == "Tut2")
            {
                textBox = 2;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.name == "Tut3")
            {
                textBox = 3;
                Destroy(collision.gameObject);
            }
        }
    }

    void Text()
    {
        string firstText = "Ooo... I think I could jump over\nthat with space... A...";
        string secondText = "I could probably make that jump\nwith a dash... shift? X?\nI cant remember...";
        string thirdText = "Am I seeing things...? I should try\nsquashing it...";

        string damageText1 = "Yeeeouch!";
        string damageText2 = "Ouch!";
        string damageText3 = "Aaaaa!";

        string fallText1 = "Nooooooooo...";
        string fallText2 = "AH... I thought I fell...";
        string fallText3 = "I could of swore...";

        if (textBox == 1)
        {
            curText = firstText;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 2)
        {
            curText = secondText;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 3)
        {
            curText = thirdText;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 4)
        {
            curText = damageText1;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 5)
        {
            curText = damageText2;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 6)
        {
            curText = damageText3;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 7)
        {
            curText = fallText1;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 8)
        {
            curText = fallText2;
            StartCoroutine(TextDisplay());
        }
        if (textBox == 9)
        {
            curText = fallText3;
            StartCoroutine(TextDisplay());
        }
    }

    void TalkingCheck()
    {
        if (talking == true)
        {
            skip = true;
        }
    }

    IEnumerator TextDisplay()
    {
        TalkingCheck();
        textBox = 0;
        talking = true;
        for (int i = 0; i < curText.Length; i++)
        {
            text.text = curText.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
            if (skip == true)
            {
                skip = false;
                break;
            }
        }
        yield return new WaitForSeconds(2f);
        text.text = textWipe;
        talking = false;
    }

}
