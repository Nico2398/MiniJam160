using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlRoom : MonoBehaviour
{
    // Gameplay parameters
    public float rotationTorque = 1f;

    // Modules parameters
    private int maxEnergy = 0;
    private int currentEnergy;
    private float maxFuel = 0f;
    private float currentFuel = 0f;
    private List<LaserCanon> laserCanons = new List<LaserCanon>();
    private List<Reactor> reactors = new List<Reactor>();
    private List<FuelTank> fuelTanks = new List<FuelTank>();

    // Input variables
    private float inputRotation = 0f;
    private float inputThrust = 0f;
    private float inputFire = 0f;
    private bool pendingBomb = false;

    // Components
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentEnergy = maxEnergy;
        currentFuel = maxFuel;
    }

    void FixedUpdate()
    {
        rb.AddTorque(-inputRotation * Time.fixedDeltaTime * rotationTorque);
        if (inputThrust > 0)
        {
            Thrust(Time.fixedDeltaTime);
        }
        if (inputFire > 0)
        {
            Fire();
        }
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

    public bool ConsumeFuel(float fuel)
    {
        if (currentFuel >= fuel)
        {
            currentFuel -= fuel;
            return true;
        }
        return false;
    }

    public void RegisterLaserCanon(LaserCanon laserCanon)
    {
        laserCanons.Add(laserCanon);
    }

    public void UnregisterLaserCanon(LaserCanon laserCanon)
    {
        laserCanons.Remove(laserCanon);
    }

    public void RegisterReactor(Reactor reactor)
    {
        reactors.Add(reactor);
    }

    public void UnregisterReactor(Reactor reactor)
    {
        reactors.Remove(reactor);
    }

    public void AddMaxFuel(float fuel)
    {
        maxFuel += fuel;
        currentFuel += fuel;
    }

    public void RemoveMaxFuel(float fuel)
    {
        maxFuel -= fuel;
        currentFuel = Mathf.Min(currentFuel, maxFuel);
    }

    private void Fire()
    {
        foreach(LaserCanon laserCanon in laserCanons)
        {
            laserCanon.Fire();
        }
    }

    private void Bomb()
    {

    }

    private void Rotate()
    {

    }

    private void Thrust(float deltaTime)
    {
        foreach(Reactor reactor in reactors)
        {
            reactor.Thrust(deltaTime);
        }
    }

    public void OnFireInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        inputFire = input;
    }

    public void OnBombInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pendingBomb = true;
        }
    }

    public void OnRotateInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        inputRotation = input;
    }

    public void OnThrustInput(InputAction.CallbackContext context)
    {
        float input = context.ReadValue<float>();
        inputThrust = input;
    }
}
