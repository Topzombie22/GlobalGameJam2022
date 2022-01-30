using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    public GameObject _audio;
    public AudioManagerPlayer manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = _audio.GetComponent<AudioManagerPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<PlayerController>().sanity != 4)
            {
                collision.GetComponent<PlayerController>().sanity += 1;
            }
            manager.Pickup();
            Destroy(gameObject);
        }
    }

}
