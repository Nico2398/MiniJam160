using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : ModuleEquipment
{
    public int size = 10;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void SetControlRoom(ControlRoom controlRoom)
    {
        base.SetControlRoom(controlRoom);
        base.controlRoom.AddCargo(this);
    }

    public void OnDestroy()
    {
        base.controlRoom.RemoveCargo(this);
    }
}
