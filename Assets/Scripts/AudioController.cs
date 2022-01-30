using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource hover1;
    public AudioSource click1;

    public AudioClip hover;
    public AudioClip click;

    // Start is called before the first frame update
    void Start()
    {
        hover1.clip = hover;
        click1.clip = click;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIHover()
    {
        Debug.Log("Moused");
        hover1.Play();
    }

    public void UIClick()
    {
        click1.Play();
    }


}
