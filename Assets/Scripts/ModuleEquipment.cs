using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleEquipment : MonoBehaviour
{
    public ControlRoom controlRoom;
    private Joint2D joint;

    // Start is called before the first frame update
    public virtual void Start()
    {
        joint = GetComponent<Joint2D>();
        if (controlRoom != null)
        {
            SetControlRoom(controlRoom);
        }
    }

    public virtual void SetControlRoom(ControlRoom controlRoom)
    {
        this.controlRoom = controlRoom;
        joint.connectedBody = controlRoom.GetComponent<Rigidbody2D>();
    }
}
