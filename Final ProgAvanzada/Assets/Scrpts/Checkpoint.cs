using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update


    public SpriteRenderer theSR;

    public Sprite cpOff, cpOn;
   
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CheckpointController.instance.DeactivateCheckpoints();
            theSR.sprite = cpOn;
            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        theSR.sprite = cpOff;
    }
}
