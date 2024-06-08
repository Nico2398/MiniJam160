using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject lootPrefab;
    public int lootMaxAmount = 3;
    public float lootIndividualProbability = 0.5f;
    public float initialSpeed = 2f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity += Random.insideUnitCircle * initialSpeed;
    }

    void OnDestroy()
    {
        for (int i = 0; i < lootMaxAmount; i++)
        {
            if (Random.value < lootIndividualProbability)
            {
                GameObject lootInstance = Instantiate(lootPrefab, transform.position, transform.rotation);
                Rigidbody2D lootRb = lootInstance.GetComponent<Rigidbody2D>();
                lootRb.velocity = rb.velocity;
            }
        }
    }
}
