using System;
using UnityEngine;
using Utility;

namespace Enemies
{
	public class PatrolBetweenPoints : MonoBehaviour
	{
		[Header("Waypoint Settings")]
		[SerializeField] private WaypointBehaviour _behaviour;
		[SerializeField] private Transform[] waypoints;
		[SerializeField] private float distanceThreshold = 0.1f;
		private int _nextWaypoint = 0;
		private int sign = 1;//sign
		private bool active = true;
		//
		[Header("Movement Settings")]
		[SerializeField] float speed;
		
		//references
		private Rigidbody2D _rigidbody2D;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}


		private void FixedUpdate()
		{
			//waypoint shuffling
			if (!active || waypoints.Length == 0)
			{
				return;
			}
			var goal = waypoints[_nextWaypoint];
			if (goal == null)
			{
				return;
			}
			if (Vector3.Distance(transform.position, goal.position) <= distanceThreshold)
			{
				CycleWaypoint();
			}
			//movement
			_rigidbody2D.MovePosition(Vector3.MoveTowards(transform.position,goal.position,speed*Time.fixedDeltaTime));
			
		}
		public void Restart()
		{
			_nextWaypoint = 0;
			active = true;
		}
		private void CycleWaypoint()
		{
			if (_behaviour == WaypointBehaviour.Loop)
			{
				_nextWaypoint = _nextWaypoint + 1;
				if (_nextWaypoint >= waypoints.Length)
				{
					_nextWaypoint = 0;
				}
			}else if (_behaviour == WaypointBehaviour.PingPing)
			{
				if (_nextWaypoint == waypoints.Length-1)
				{
					sign = -1;
				}else if (_nextWaypoint == 0)
				{
					sign = 1;
				}
				_nextWaypoint = _nextWaypoint + sign;
			}else if (_behaviour == WaypointBehaviour.StopAtEnd)
			{
				if (_nextWaypoint < waypoints.Length - 1)
				{
					_nextWaypoint++;
				}
				else
				{
					active = false;
				}
			}
		}
	}
}