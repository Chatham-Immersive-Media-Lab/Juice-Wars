using System;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.Events;

namespace Feedbacks
{
	[Serializable]
	public class Feedback
	{
		public GameObject PrefabToSpawn;
		public AudioSource AudioSource;
		public UnityEvent Event;

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
		}
	}
}