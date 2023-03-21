using Feedbacks;
using UnityEngine;

namespace Agents
{
	//I am basically re-writing code as enemyBase. We should all be agents, and agents should have health. oops.
	public class Player : MonoBehaviour
	{
		public float Health => _health;
		private float _health;
		[SerializeField] private Feedback onTakeDamage;
		[SerializeField] private Feedback onDie;

		public void TakeDamage(float damage, Vector3 impactLocation)
		{
			_health -= _health;
			onTakeDamage.Invoke(impactLocation);
			if(_health <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			onDie.Invoke(transform.position);
			Destroy(gameObject);
		}
	}
}