using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : ModuleEquipment
{
    public float thrustForce = 10f;
    public float fuelConsumption = 1f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Thrust(float deltaTime)
    {
        if (controlRoom.ConsumeFuel(fuelConsumption * deltaTime))
            rb.AddForce(transform.up * thrustForce * deltaTime);
    }

    public override void SetControlRoom(ControlRoom controlRoom)
    {
        base.SetControlRoom(controlRoom);
        controlRoom.RegisterReactor(this);
    }

    public void OnDestroy()
    {
        controlRoom.UnregisterReactor(this);
    }


}
