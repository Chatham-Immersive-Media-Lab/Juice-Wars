using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIUpdateProgressBarWithHealth : MonoBehaviour
	{
		[SerializeField] private Agent _agent;
		private Slider _slider;
		
		private void Update()
		{
			//would love events to subscribe to when health changes....
			//but here we can just swap agents out at runtime. which might be useful i guess.
			if (_slider != null && _agent != null)
			{
				_slider.value = _agent.GetPercentageHealth();
			}
		}
	}
}