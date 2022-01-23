using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour
{
    [SerializeField]
    private Image fadeAble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(StartFade());
    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartFade()
    {
        fadeAble.gameObject.SetActive(true);
        fadeAble.CrossFadeAlpha(255f, 2f, false);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("GameScene");
    }

}
