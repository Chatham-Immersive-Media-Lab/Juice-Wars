﻿using Feedbacks;
using System;
using UnityEngine;
using Weapons;

namespace Enemies
{
	public class FireAtPlayer : MonoBehaviour
	{
		[SerializeField] private bool _active = true;
		[SerializeField] private Weapon _weapon;
		[SerializeField] private Transform bulletSpawnLocation;
		private Transform _player;
		public Feedback _fireFeedback;

		private void Start()
		{
			_player = GameObject.FindGameObjectWithTag("Player").transform;
			_weapon.InitWeapon();
		}

		private void Update()
		{
			_weapon.Tick();
			if (_active && _player != null)
			{
				var spawnLocation = bulletSpawnLocation == null ? transform.position : bulletSpawnLocation.position;
				var playerDirection = _player.position - transform.position;
				//fire continuously, let weapon cooldown determine firing rate.
				if (_weapon.Fire(spawnLocation, playerDirection))
				{
                    _fireFeedback.Invoke(spawnLocation);
                }
			}
		}

		public void SetFiring(bool firing)
		{
			_active = firing;
		}
	}
}