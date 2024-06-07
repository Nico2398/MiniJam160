using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Repair()
    {
        currentHealth = maxHealth;
    }

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
