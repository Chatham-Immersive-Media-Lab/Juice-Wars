using System;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.Events;

namespace Feedbacks
{
	[Serializable]
	public class Feedback
	{
		[Header("Optional Events")]
		public UnityEvent Event;
		[Header("Optional Prefab Instantiation Settings")]
		public GameObject PrefabToSpawn;
		[Header("Optional Audio Settings")]
		public AudioSource AudioSource;
		[Header("Optional Animator Settings")]
		public Animator Animator;

		public string TriggerName;
		
		public void Invoke(Vector3 position)
		{
			Event.Invoke();
			if (PrefabToSpawn != null)
			{
				GameObject.Instantiate(PrefabToSpawn, position, PrefabToSpawn.transform.rotation);
			}

			if (AudioSource != null)
			{
				AudioSource.Play();
			}

			if (Animator != null)
			{
				Animator.SetTrigger(TriggerName);	
			}
		}
	}
}