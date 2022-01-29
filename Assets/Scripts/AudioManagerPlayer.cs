using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerPlayer : MonoBehaviour
{
    public AudioSource jump;
    public AudioSource dash;
    public AudioSource fall;
    public AudioSource aboveGroundTheme;
    public AudioSource underGroundTheme;
    public AudioSource takeDamage;
    public AudioSource landing;


    public AudioClip _jump;
    public AudioClip _dash;
    public AudioClip _fall;
    public AudioClip _aboveGroundTheme;
    public AudioClip _underGroundTheme;
    public AudioClip _takeDamage;
    public AudioClip _landing;

    [SerializeField]
    private bool hasFaded;


    // Start is called before the first frame update
    void Start()
    {
        jump.clip = _jump;
        dash.clip = _dash;
        fall.clip = _fall;
        aboveGroundTheme.clip = _aboveGroundTheme;
        underGroundTheme.clip = _underGroundTheme;
        takeDamage.clip = _takeDamage;
        landing.clip = _landing;

        PlayAbove();
        PlayUnder();
    }

    // Update is called once per frame
    void Update()
    {
        aboveGroundTheme.volume = Mathf.Lerp(aboveGroundTheme.volume, 0.5f, 0.5f * Time.deltaTime);
        if (hasFaded == false)
        {


            if (aboveGroundTheme.volume == 1f)
            {
                hasFaded = true;
            }
        }

    }

    public void PlayLanding()
    {
        landing.Play();
    }

    public void PlayDash()
    {
        dash.Play();
    }

    public void PlayJump()
    {
        jump.Play();
    }

    public void PlayFall()
    {
        fall.Play();
    }
    
    public void PlayAbove()
    {
        aboveGroundTheme.Play();
    }

    public void PlayUnder()
    {
        underGroundTheme.Play();
    }

    public void PlayTakeDamage()
    {
        takeDamage.Play();
    }

}
