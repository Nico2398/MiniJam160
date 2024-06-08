using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public float animationDuration = 2f;
    public float animationScale = 4f;
    private float animationTime = 0;
    public float attractionForce = 10f;
    private Vector3 initialScale;
    private Rigidbody2D playerRb;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if (animationTime > 0)
        {
            animationTime -= Time.fixedDeltaTime;
            if (animationTime <= 0)
            {
                Debug.Log("Wormhole animation finished");
            }
            float t = 1 - animationTime / animationDuration;
            transform.localScale = initialScale * Mathf.Lerp(1, animationScale, t*t);
            Vector2 direction = (Vector2)transform.position - playerRb.position;
            playerRb.AddForce(direction.normalized * attractionForce * t*t);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (animationTime > 0)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            ControlRoom controlRoom = collision.gameObject.GetComponent<ControlRoom>();
            if (controlRoom != null)
            {
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                animationTime = animationDuration;
            }
        }
    }
}
