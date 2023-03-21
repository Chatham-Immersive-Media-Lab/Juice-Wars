using System;
using UnityEngine;

namespace Agents
{
	//This script only exists because I want the player script on the parent object and the collider on a child :p
	public class PlayerColliderHandler : MonoBehaviour
	{

		private Player _player;

		private void Awake()
		{
			_player = GetComponentInParent<Player>();
		}

		private void OnCollisionEnter2D(Collision2D col)
		{
			var bullet = col.gameObject.GetComponent<Bullet>();
			if (bullet != null)
			{
				_player.TakeDamage(bullet.damage,col.GetContact(0).point);
			}
		}
	}
}