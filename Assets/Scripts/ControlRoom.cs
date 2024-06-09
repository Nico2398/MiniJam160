using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ControlRoom : MonoBehaviour
{
    // Gameplay parameters
    public float rotationTorque = 1f;
    public GameObject[] lootPrefabs;
    public GameObject lootMagnet;

    // UI parameters
    public Image fuelBar;
    public Image energyBar;

    // Modules parameters
    private float maxEnergy = 0;
    private float currentEnergy;
    private float energyRate = 0f;
    private float maxFuel = 0f;
    private float currentFuel = 0f;
    private int maxCargo = 0;
    private int currentCargo = 0;
    private Dictionary<LootType, int> cargoAmountByType = new Dictionary<LootType, int>();
    private List<LaserCanon> laserCanons = new List<LaserCanon>();
    private List<Reactor> reactors = new List<Reactor>();

    // Input variables
    private float inputRotation = 0f;
    private float inputThrust = 0f;
    private float inputFire = 0f;
    private bool pendingBomb = false;

    // Components
    private Rigidbody2D rb;

    // Internal variables
    private Dictionary<LootType, GameObject> lootPrefabsByType;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentEnergy = maxEnergy;
        currentFuel = maxFuel;

        InitLootPrefabs();
    }

    private void Update()
    {
        fuelBar.fillAmount = currentFuel / maxFuel;
        energyBar.fillAmount = currentEnergy / maxEnergy;
    }

    private void InitLootPrefabs()
    {
        lootPrefabsByType = new Dictionary<LootType, GameObject>();
        foreach(GameObject lootPrefab in lootPrefabs)
        {
            Loot loot = lootPrefab.GetComponent<Loot>();
            lootPrefabsByType[loot.lootType] = lootPrefab;
        }
        LootType[] allLootTypes = (LootType[])Enum.GetValues(typeof(LootType));
        bool containsAllLootTypes = allLootTypes.All(type => lootPrefabsByType.ContainsKey(type));
        if (!containsAllLootTypes)
        {
            Debug.LogError("lootPrefabs is missing some types of loot.");
        }
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
        if (pendingBomb)
        {
            Bomb();
            pendingBomb = false;
        }
        currentEnergy = Mathf.Min(currentEnergy + energyRate * Time.fixedDeltaTime, maxEnergy);
    }

    public bool ConsumeEnergy(float consumedEnergy)
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

    public void AddMaxEnergy(float energy)
    {
        maxEnergy += energy;
        currentEnergy += energy;
    }

    public void RemoveMaxEnergy(float energy)
    {
        maxEnergy -= energy;
        currentEnergy = Mathf.Min(currentEnergy, maxEnergy);
    }

    public void AddEnergyRate(float rate)
    {
        energyRate += rate;
    }

    public void RemoveEnergyRate(float rate)
    {
        energyRate -= rate;
    }

    public void AddCargoRoom(int capacity)
    {
        maxCargo += capacity;
        UpdateMagnet();
    }

    public void RemoveCargoRoom(int capacity)
    {
        maxCargo -= capacity;
        DropExcessCargo();
        UpdateMagnet();
    }

    public void DropExcessCargo()
    {
        while (currentCargo > maxCargo)
        {
            int cargoToDropId = UnityEngine.Random.Range(0, currentCargo);
            LootType typeToDrop = LootType.Scrap;
            int currentId = 0;
            foreach(LootType lootType in cargoAmountByType.Keys)
            {
                currentId += cargoAmountByType[lootType];
                if (currentId >= cargoToDropId)
                {
                    typeToDrop = lootType;
                    break;
                }
            }
            Drop(typeToDrop);
        }
    }

    public bool Loot(LootType lootType)
    {
        if (currentCargo < maxCargo)
        {
            if (!cargoAmountByType.ContainsKey(lootType))
            {
                cargoAmountByType[lootType] = 0;
            }
            cargoAmountByType[lootType]++;
            currentCargo++;
        }
        else
        {
            return false;
        }
        UpdateMagnet();
        return true;
    }

    public void Drop(LootType lootType)
    {
        if (cargoAmountByType.ContainsKey(lootType) && cargoAmountByType[lootType] > 0)
        {
            cargoAmountByType[lootType]--;
            currentCargo--;
            if (cargoAmountByType[lootType] == 0)
            {
                cargoAmountByType.Remove(lootType);
            }

            GameObject lootObject = Instantiate(lootPrefabsByType[lootType], transform.position, transform.rotation);
            Loot loot = lootObject.GetComponent<Loot>();
            loot.LockMagnet();
            loot.Reject();
        }
        UpdateMagnet();
    }

    private void UpdateMagnet()
    {
        lootMagnet.SetActive(maxCargo > currentCargo);
        Debug.Log("Magnet is " + (maxCargo > currentCargo ? "on" : "off") + "(" + currentCargo + "/" + maxCargo + ")");
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
        Debug.Log("Bomb dropped!");
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
