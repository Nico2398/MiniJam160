using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCanon : ModuleEquipment
{
    public float energyCost = 5;
    public GameObject laserPrefab;

    public void Fire()
    {
        if (controlRoom.ConsumeEnergy(energyCost))
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, transform.rotation);
            Laser laserScript = laser.GetComponent<Laser>();
            laserScript.SetEmitter(gameObject);
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
