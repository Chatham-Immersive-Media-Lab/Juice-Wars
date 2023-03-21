using System;
using UnityEngine;
using Weapons;

namespace Enemies
{
	public class FireConstantlyInDirection : MonoBehaviour
	{
		[SerializeField] private Space directionSpace;
		[SerializeField] private Vector2 direction;
		[SerializeField] private Weapon _weapon;
		[SerializeField] private Transform bulletSpawnLocation;
		private void Start()
		{
			_weapon.InitWeapon();
		}

		private void Update()
		{
			_weapon.Tick();

			var spawnLocation = bulletSpawnLocation == null ? transform.position : bulletSpawnLocation.position;
			if (directionSpace == Space.World)
			{
				_weapon.Fire(spawnLocation,direction);
			}
			else
			{
				var dir = transform.TransformDirection(direction);
				_weapon.Fire(spawnLocation, dir);
			}
		}
	}
}