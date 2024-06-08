using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damage = 2;
    public float speed = 3f;
    public float timout = 10f;
    GameObject emitter;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity += new Vector2(transform.up.x, transform.up.y) * speed;
    }

    public void SetEmitter(GameObject emitter)
    {
        this.emitter = emitter;
    }

    void FixedUpdate()
    {
        timout -= Time.fixedDeltaTime;
        if (timout <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == emitter || other.isTrigger)
        {
            return;
        }

        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.ReceiveDamage(damage);
        }

        Destroy(gameObject);
    }
}
