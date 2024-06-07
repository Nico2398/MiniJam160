using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : ModuleEquipment
{
    public float maxFuel = 90f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void SetControlRoom(ControlRoom controlRoom)
    {
        base.SetControlRoom(controlRoom);
        controlRoom.AddMaxFuel(maxFuel);
    }

    public void OnDestroy()
    {
        controlRoom.RemoveMaxFuel(maxFuel);
    }
}
