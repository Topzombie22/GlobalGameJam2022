using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerPlayer : MonoBehaviour
{
    public AudioSource footSteps;
    public AudioSource jump;
    public AudioSource dash;
    public AudioSource fall;
    public AudioSource aboveGroundTheme;
    public AudioSource underGroundTheme;


    public AudioClip _footSteps;
    public AudioClip _jump;
    public AudioClip _dash;
    public AudioClip _fall;
    public AudioClip _aboveGroundTheme;
    public AudioClip _underGroundTheme;

    [SerializeField]
    private bool hasFaded;


    // Start is called before the first frame update
    void Start()
    {
        footSteps.clip = _footSteps;
        jump.clip = _jump;
        dash.clip = _dash;
        fall.clip = _fall;
        aboveGroundTheme.clip = _aboveGroundTheme;
        underGroundTheme.clip = _underGroundTheme;

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

    public void PlayDash()
    {
        dash.Play();
    }

    public void PlayJump()
    {
        jump.Play();
    }

    public void PlayFootsteps()
    {
        footSteps.Play();
    }

    public void StopFootsteps()
    {
        footSteps.Stop();
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

}
