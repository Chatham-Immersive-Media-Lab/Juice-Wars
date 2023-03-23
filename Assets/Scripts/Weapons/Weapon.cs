using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Weapons
{
	[CreateAssetMenu(fileName = "Weapon", menuName = "TopDown/Weapon", order = 0)]
	public class Weapon : ScriptableObject
	{
		//Editable in Unity
		[Header("Bullet Settings")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private float bulletSpeed;
		[Min(0)] [SerializeField] private float maxFireFrequency;
		//
		[Header("Pool Settings")]
		[SerializeField] private bool usePooler = true;
		[SerializeField] private int prepopulatePoolCount = 10;
		[SerializeField] private AudioClip _weaponSound;
        //private variables
        private List<Bullet> _pool;
		private float _fireTimer;
		public void InitWeapon()
		{
			_pool = new List<Bullet>();
			_fireTimer = maxFireFrequency;
			if (usePooler)
			{
				PrepopulatePool(prepopulatePoolCount);
			}
		}
  
        public virtual bool Fire(Vector3 position, Vector2 direction)
		{
			if (_fireTimer >= maxFireFrequency)
			{
				Bullet bullet = CreateBullet(position, Quaternion.FromToRotation(Vector2.right, direction));
				Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
				bulletRB.velocity = direction.normalized * bulletSpeed;
				_fireTimer = 0;
                return true;
            }
			return false;
		}

		private void PrepopulatePool(int numberOfBulletsInPool)
		{
			for (int i = 0; i < numberOfBulletsInPool; i++)
			{
				GameObject newPooledObject = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
				Bullet newBullet = newPooledObject.GetComponent<Bullet>();
				_pool.Add(newBullet);
				newPooledObject.gameObject.SetActive(false);
			}
		}
		public Bullet CreateBullet(Vector3 position, Quaternion rotation)
		{
			if (usePooler)
			{
				foreach (var bullet in _pool)
				{
					if (!bullet.gameObject.activeInHierarchy)
					{
						bullet.ResetBullet();
						bullet.transform.position = position;
						bullet.transform.rotation = rotation;
						return bullet;
					}
				}
			}

			//when we dont have one in the pool... OR we just aren't using a pooler.
			GameObject newPooledObject = Instantiate(bulletPrefab, position, rotation);
			Bullet newBullet = newPooledObject.GetComponent<Bullet>();
			
			
			if (usePooler)
			{
				_pool.Add(newBullet);
			}

			return newBullet;
		}

		public void Tick()
		{
			_fireTimer = _fireTimer + Time.deltaTime;
		}
	}
}