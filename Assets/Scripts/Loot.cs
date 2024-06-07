using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootType
{
    Scrap,
    Combustium,
    Energium,
}

public class Loot : MonoBehaviour
{
    public float magnetForce = 20f;
    public LootType lootType;

    private Rigidbody2D rb;
    private GameObject magnet;
    private float magnetRange;
    private bool lockedMagnet = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LockMagnet()
    {
        lockedMagnet = true;
    }

    public void SetMagnet(GameObject magnet, float magnetRange)
    {
        if (lockedMagnet)
        {
            return;
        }
        this.magnet = magnet;
        this.magnetRange = magnetRange;
    }
    public void RemoveMagnet()
    {
        this.magnet = null;
    }

    private void FixedUpdate()
    {
        if (magnet != null)
        {
            Vector3 direction = magnet.transform.position - transform.position;
            float relativeDistance = direction.magnitude / magnetRange;
            float weight = Mathf.Clamp(1 - (relativeDistance * relativeDistance), 0, 1);
            rb.AddForce(direction.normalized * magnetForce * weight);
        }
    }

    public void Reject()
    {
        rb.AddForce(Random.insideUnitCircle * magnetForce);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlRoom controlRoom = other.GetComponent<ControlRoom>();
            if (controlRoom != null)
            {
                if (controlRoom.Loot(lootType))
                    Destroy(gameObject);
            }
        }
    }
}
