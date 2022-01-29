using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DeathCam();
    }

    private void DeathCam()
    {
        if (cam.position.y < -2f)
        {
            var vcam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
            vcam.LookAt = null;
            vcam.Follow = null;
        }
        if (player.position.y > -2f)
        {
            var vcam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
            vcam.LookAt = player;
            vcam.Follow = player;
        }
    }

}
