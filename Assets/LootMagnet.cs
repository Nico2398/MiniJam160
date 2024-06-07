using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootMagnet : MonoBehaviour
{
    private float magnetRange;

    void Start()
    {
        CircleCollider2D magnetCollider = GetComponent<CircleCollider2D>();
        magnetRange = magnetCollider.radius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Loot loot = collision.GetComponent<Loot>();
        if (loot != null)
        {
            loot.SetMagnet(gameObject, magnetRange);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Loot loot = collision.GetComponent<Loot>();
        if (loot != null)
        {
            loot.RemoveMagnet();
        }
    }
}
