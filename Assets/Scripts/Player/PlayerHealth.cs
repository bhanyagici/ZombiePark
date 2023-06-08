using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void DeductHealth(int damage) 
    {
        currentHealth -= damage;
        Debug.Log("Playerýn caný azaldý " + currentHealth);
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        print("Player Öldü");
    }

    public void AddHealth(int value) 
    {
        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }


   
}
