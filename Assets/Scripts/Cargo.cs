using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : ModuleEquipment
{
    public int capacity = 10;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void SetControlRoom(ControlRoom controlRoom)
    {
        base.SetControlRoom(controlRoom);
        base.controlRoom.AddCargoRoom(capacity);
    }

    public void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) // Destruction is caused by the scene being unloaded
            return;

        base.controlRoom.RemoveCargoRoom(capacity);
    }
}
