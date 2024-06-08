using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCanon : ModuleEquipment
{
    public float energyCost = 5;
    public GameObject laserPrefab;
    public float cooldownMax = 0.5f;

    private float cooldown = 0;
    private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        cooldown -= Time.fixedDeltaTime;
    }

    public void Fire()
    {
        if (cooldown > 0)
        {
            return;
        }
        if (controlRoom.ConsumeEnergy(energyCost))
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            Laser laserScript = laser.GetComponent<Laser>();
            laserScript.SetEmitter(gameObject);
            laser.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            cooldown = cooldownMax;
        }
    }

    public override void SetControlRoom(ControlRoom controlRoom)
    {
        base.SetControlRoom(controlRoom);
        controlRoom.RegisterLaserCanon(this);
    }

    public void OnDestroy()
    {
        controlRoom.UnregisterLaserCanon(this);
    }
}
