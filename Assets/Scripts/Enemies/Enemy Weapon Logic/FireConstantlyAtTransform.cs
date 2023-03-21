using UnityEngine;
using Weapons;

namespace Enemies
{
	public class FireConstantlyAtTransform : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private Weapon _weapon;
		[SerializeField] private Transform bulletSpawnLocation;

		private void Start()
		{
			_weapon.InitWeapon();
		}

		private void Update()
		{
			_weapon.Tick();
			if (target == null)
			{
				return;
			}
			var spawnLocation = bulletSpawnLocation == null ? transform.position : bulletSpawnLocation.position;
			_weapon.Fire(spawnLocation, target.position-transform.position);
		}

		public void SetTarget(Transform target)
		{
			this.target = target;
		}
	}
}