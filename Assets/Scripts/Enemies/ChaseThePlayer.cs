using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Enemies
{
	public class ChaseThePlayer : MonoBehaviour
	{
		
		private Transform _player;
		[Header("Movement Settings")]
		[Min(0)]
		[SerializeField] private float speed;
		[Min(0)]
		[SerializeField] private float minimumDistance = 0.2f;

		[SerializeField] [Min(0)] private float rotationDegreesPerSecond = 10;
		[Header("Search For Player Settings")]
		[Min(0)]
		[SerializeField] private float maxSearchDistance = 100;
		[Min(0.01f)]
		[SerializeField] private float delayBetweenSearches = 0.2f;
		[FormerlySerializedAs("_layerMask")]
		[SerializeField]
		[Tooltip("Must contain player and line-of-sight blocking environment layers.")]
		private LayerMask _castForPlayerLayerMask;
		//
		
		private Vector3 currentGoalPosition;
		private bool foundPlayer;
		private bool isActive;
		private Coroutine searchRoutine;
		//

		private FireAtPlayer _weaponHandler;
		private Rigidbody2D _rigidbody2D;
		private void Awake()
		{
			_weaponHandler = GetComponent<FireAtPlayer>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			isActive = true;
			foundPlayer = false;
			//I don't love using the gameobject.find functions, but whatever.
			_player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		private void OnEnable()
		{
			searchRoutine = StartCoroutine(SlowlySearchForPlayer());
		}

		private void OnDisable()
		{
			if (searchRoutine != null)
			{
				StopCoroutine(searchRoutine);
			}
		}

		private void FixedUpdate()
		{
			if (Vector3.Distance(transform.position, currentGoalPosition) <= minimumDistance)
			{
				return;
			}

			//movement
			_rigidbody2D.MovePosition(Vector3.MoveTowards(transform.position, currentGoalPosition, speed * Time.fixedDeltaTime));

			var desiredFacing = (currentGoalPosition - transform.position).normalized;
			var angleToFacing = Vector3.Angle(transform.transform.right, desiredFacing);
			_rigidbody2D.MoveRotation(angleToFacing*Time.fixedDeltaTime * rotationDegreesPerSecond);
		}


		public IEnumerator SlowlySearchForPlayer()
		{
			while (isActive)
			{
				SearchForPlayer();
				yield return new WaitForSeconds(delayBetweenSearches);
			}
		}
		public void SearchForPlayer()
		{
			RaycastHit2D hit;
			hit = Physics2D.Raycast(transform.position, _player.position - transform.position, maxSearchDistance,_castForPlayerLayerMask);
			if (hit.collider != null)
			{
				//thing we hit or it's parent object is the player.
				if (hit.collider.gameObject.CompareTag("Player") || hit.collider.transform.parent != null && hit.collider.transform.parent.CompareTag("Player"))
				{
					currentGoalPosition = hit.transform.position;
					foundPlayer = true;
					if (_weaponHandler != null)
					{
						_weaponHandler.SetFiring(true);
					}
				}
				else
				{
					if (hit.transform.gameObject == transform.gameObject)
					{
						Debug.LogWarning("Chase enemy did a raycast for the player but found itself! We probably want to fiddle with layer settings. (is this enemy on an enemy layer that the raycast is ignoring?)");
					}
					foundPlayer = false;
					//stop moving
					currentGoalPosition = transform.position;
					if (_weaponHandler != null)
					{
						_weaponHandler.SetFiring(false);
					}
				}
			}
			else
			{
				foundPlayer = false;
			}
		}
		
	}
}