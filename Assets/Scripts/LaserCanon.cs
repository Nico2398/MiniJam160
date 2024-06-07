using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCanon : ModuleEquipment
{
    public int energyCost = 10;

    public void Fire()
    {
        if (controlRoom.ConsumeEnergy(energyCost))
        {
            Debug.Log("Laser fired!");
        }
        else
        {
            Debug.Log("Not enough energy to fire the laser!");
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
