using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    [SerializeField]
    private Image fadeAble;
    [SerializeField]
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        fadeAble.CrossFadeAlpha(0f, 2.5f, false);
        yield return new WaitForSeconds(2.5f);
        fadeAble.gameObject.SetActive(false);
    }
}
