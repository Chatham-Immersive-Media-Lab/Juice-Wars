using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Player Config")]
    [SerializeField] private Transform bulletSpawnLocation;

    [Header("Weapon Config")] [SerializeField]
    private Weapon[] _weapons;

    private int currentWeaponIndex = 0;
    
    
    private void Start()
    {
        if (_weapons.Length == 0)
        {
           
        }
        
        foreach (var weapon in _weapons)
        {
            weapon.InitWeapon();
        }
    }

    public void Fire(Vector2 direction)
    {
        if (_weapons.Length == 0)
        {
            return;
        }

        //ternery operator to choose the bulletSpawnLocation, or our transform.position if its null. 
        var spawnLocation = bulletSpawnLocation == null ? transform.position : bulletSpawnLocation.position;
        _weapons[currentWeaponIndex].Fire(spawnLocation, direction);
    }

    public void SwitchToNextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= _weapons.Length)
        {
            currentWeaponIndex = 0;
        }
    }

    public void SwitchToPrevWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = _weapons.Length-1;
        }
    }

    private void Update()
    {
        if (_weapons.Length == 0)
        {
            return;
        }
        _weapons[currentWeaponIndex].Tick();
    }
}
