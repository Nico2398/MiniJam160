using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damage = 2;
    public float speed = 3f;
    GameObject emitter;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    public void SetEmitter(GameObject emitter)
    {
        this.emitter = emitter;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == emitter)
        {
            return;
        }

        Module module = other.GetComponent<Module>();
        if (module != null)
        {
            module.ReceiveDamage(damage);
        }

        Destroy(gameObject);
    }
}
