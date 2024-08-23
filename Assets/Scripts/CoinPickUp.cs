using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSFX;
    int pointForPickUp = 100;
    bool isPickup = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&&!isPickup)
        {
            isPickup = true;
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddScore(pointForPickUp);
            Destroy(gameObject);
        }
    }
    
}
