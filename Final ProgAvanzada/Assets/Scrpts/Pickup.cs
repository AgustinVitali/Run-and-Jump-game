using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;

    private bool isCollected;

    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !isCollected)
        {
            if(isGem)
            {
                LavelManager.instance.gemsCollected++;

                UIController.instance.UpdateGemCount(); 

                Instantiate(pickupEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(6);
                isCollected = true;
                Destroy(gameObject);
            }   
        if(isHeal)
            {
                if(PlayerHealthController.instance.currentHealth != PlayerHealthController.instance.maxHealth)  
                {
                    PlayerHealthController.instance.HealPlayer();
                    AudioManager.instance.PlaySFX(7);
                    isCollected = true;
                    Destroy(gameObject);
                }
            }
        }   
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
