using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectItems : MonoBehaviour
{
    PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HealthItem"))
        {
            playerHealth.AddHealth(10);
            other.gameObject.SetActive(false);
            Debug.Log("10 can eklendi " + playerHealth.GetHealth());
        }
    }


    
}
