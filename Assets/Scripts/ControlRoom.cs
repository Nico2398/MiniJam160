using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlRoom : MonoBehaviour
{
    private int maxEnergy = 0;
    private int currentEnergy;
    private float fuel = 0f;

    List<LaserCanon> laserCanons = new List<LaserCanon>();

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ConsumeEnergy(int consumedEnergy)
    {
        if (currentEnergy >= consumedEnergy)
        {
            currentEnergy -= consumedEnergy;
            return true;
        }
        return false;
    }

    public void RegisterLaserCanon(LaserCanon laserCanon)
    {
        laserCanon.SetControlRoom(this);
        laserCanons.Add(laserCanon);
    }

    public void UnregisterLaserCanon(LaserCanon laserCanon)
    {
        laserCanons.Remove(laserCanon);
    }

    public void Fire()
    {
        foreach(LaserCanon laserCanon in laserCanons)
        {
            laserCanon.Fire();
        }
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Fire");
            Fire();
        }
    }

    public void OnBombInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Bomb");
        }
    }

    public void OnRotateInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        Debug.Log(input);
    }

    public void OnThrustInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        Debug.Log(input);
    }
}
