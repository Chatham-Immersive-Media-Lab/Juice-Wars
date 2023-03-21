using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Feedbacks;
using UnityEngine;
using UnityEngine.Events;
public class EnemyBase : MonoBehaviour
{
	[Header("Enemy Settings")]
	private float health = 10;

	[Tooltip("Leave at 0 to just destroy the object instantly.")]
	[SerializeField] private float secondsToWaitAfterDeathToDestroy = 0;
	[Header("Events")]
	public Feedback OnTakeDamage;
	public Feedback OnDie;

	//references
	private Collider2D _collider2D;
	//
	private void Awake()
	{
		_collider2D = GetComponentInChildren<Collider2D>();
	}

	public virtual void OnCollisionEnter2D(Collision2D other)
	{
		var bullet = other.gameObject.GetComponent<Bullet>();
		if (bullet != null)
		{
			TakeDamage(bullet.damage,other.GetContact(0).point);
		}
	}

	public void TakeDamage(float damage,Vector3 location)
	{
		//can't take a negative damage
		damage = Mathf.Max(0, damage);
		if (damage == 0)
		{
			//idk maybe we have a shield or modifier or something.
			return;
		}
		health = health - damage;
		OnTakeDamage.Invoke(location);
		if (health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		OnDie.Invoke(transform.position);
		_collider2D.enabled = false;
		Invoke(nameof(Delete),secondsToWaitAfterDeathToDestroy);
	}

	private void Delete()
	{
		Destroy(gameObject);
	}
}
