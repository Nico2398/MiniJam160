using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleEquipment : MonoBehaviour
{
    public ControlRoom controlRoom;
    private Joint2D joint;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<Joint2D>();
        if (controlRoom != null)
        {
            joint.connectedBody = controlRoom.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetControlRoom(ControlRoom controlRoom)
    {
        this.controlRoom = controlRoom;
        joint.connectedBody = controlRoom.GetComponent<Rigidbody2D>();
    }
}
