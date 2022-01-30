using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMover : MonoBehaviour
{
    [SerializeField]
    private int checkPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().currCheckpoint = checkPoint;
        }
    }
}
