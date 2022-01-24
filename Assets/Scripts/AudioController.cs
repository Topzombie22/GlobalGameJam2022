using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioSource _audio2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIHover()
    {
        Debug.Log("Moused");
        _audio2.Play();
    }

    public void UIClick()
    {
        _audio.Play();
    }


}
