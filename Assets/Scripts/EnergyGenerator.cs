using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : ModuleEquipment
{
    public float maxEnergy = 10f;
    public float energyRate = 1f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void SetControlRoom(ControlRoom controlRoom)
    {
        base.SetControlRoom(controlRoom);
        controlRoom.AddMaxEnergy(maxEnergy);
        controlRoom.AddEnergyRate(energyRate);
    }

    public void OnDestroy()
    {
        controlRoom.RemoveMaxEnergy(maxEnergy);
        controlRoom.RemoveEnergyRate(energyRate);
    }
}
