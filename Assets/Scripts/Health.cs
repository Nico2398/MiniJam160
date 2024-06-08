using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public float invincibilityTime = .0f;
    public float blinkTime = .2f;
    public Color blinkColor = Color.red;

    private int currentHealth;
    private float currentInvincibilityTime = 0;
    private float currentBlinkTime = 0;
    private SpriteRenderer spriteRenderer;
    private Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
    }

    private void FixedUpdate()
    {
        if (currentInvincibilityTime > 0)
        {
            currentInvincibilityTime -= Time.fixedDeltaTime;
        }
        if (currentBlinkTime > 0)
        {
            currentBlinkTime -= Time.fixedDeltaTime;
            if (currentBlinkTime <= 0)
            {
                spriteRenderer.color = initialColor;
            }
        }
    }

    public void Repair()
    {
        currentHealth = maxHealth;
    }

    public void ReceiveDamage(int damage)
    {
        if (currentInvincibilityTime > 0)
        {
            return;
        }
        currentHealth -= damage;
        currentInvincibilityTime = invincibilityTime;
        if (blinkTime > 0)
        {
            currentBlinkTime = blinkTime;
            spriteRenderer.color = blinkColor;
        }
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
