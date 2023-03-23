using Cinemachine;
using Feedbacks;
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

    public Feedback feedback;
    public CinemachineImpulseSource _recoilCameraImpulseSource;//THis is the recoil camera impulse source, bill.
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
        if (_weapons[currentWeaponIndex].Fire(spawnLocation, direction))
        {
            feedback.Invoke(transform.position);
            _recoilCameraImpulseSource.GenerateImpulseAtPositionWithVelocity(spawnLocation, -direction);
        };

    }

    //This switches to the next weapon (in the _weapons array). If you only have one weapon, it won't do anything, BILL
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
