using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Feedbacks;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// An agent is a thing that has health and can die. Like players or enemies.
/// </summary>
public class Agent : MonoBehaviour
{
	[Header("Enemy Settings")] 
	[SerializeField] private float maxHealth;
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
		health = maxHealth;
		_collider2D = GetComponentInChildren<Collider2D>();
	}

	public float GetPercentageHealth()
	{
		if (maxHealth == 0)
		{
			return 0;
		}
		return health / maxHealth;
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
