using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    
    public static CheckpointController instance;    
    public Checkpoint[] checkpoints;
    public Vector3 spawnPoint;
    private void Awake()
    {
        instance = this;
    }   
    void Start()
    {
        spawnPoint = PlayerController.instance.transform.position;
    }
    public void DeactivateCheckpoints()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }   
    public void RespawnPlayer()
    {
        PlayerController.instance.transform.position = spawnPoint;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
        PlayerController.instance.gameObject.SetActive(true);
        PlayerHealthController.instance.theSR.color = new Color(PlayerHealthController.instance.theSR.color.r, PlayerHealthController.instance.theSR.color.g, PlayerHealthController.instance.theSR.color.b, 1f);
    }   




    // Update is called once per frame
    void Update()
    {
        
    }
}
