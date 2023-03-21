using System;
using UnityEngine;
using UnityEngine.Events;

namespace Utility
{
	public class UnityEventsCall : MonoBehaviour
	{
		public UnityEvent StartEvent;
		public UnityEvent OnEnableEvent;
		public UnityEvent OnDisableEvent;
		public UnityEvent OnDestroyEvent;

		public void Start()
		{
			StartEvent.Invoke();
		}

		public void OnEnable()
		{
			OnEnableEvent.Invoke();
		}

		public void OnDisable()
		{
			OnDisableEvent.Invoke();
		}

		public void OnDestroy()
		{
			OnDestroyEvent.Invoke();
		}
	}
}