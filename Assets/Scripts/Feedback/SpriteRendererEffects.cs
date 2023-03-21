using System.Collections;
using UnityEngine;

namespace Feedbacks
{
	public class SpriteRendererEffects : MonoBehaviour
	{
		private SpriteRenderer _spriteRenderer;

		[Header("Flash/Blink settings")]
		[SerializeField]
		private Color flashColor = Color.black;
		[SerializeField]private float _flashColorTime = 0.1f;
		[SerializeField] private float _flashOffTime = 0.1f;
		[SerializeField] private int _flashTimes = 1;

		[Header("Color Fade Settings")]
		[SerializeField] private Color fadeTargetColor;
		[SerializeField] private float timeToFade;
		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		/// <summary>
		/// Settings for flashing are serialized.
		/// </summary>
		public void Flash()
		{
			StartCoroutine(DoFlash());
		}

		IEnumerator DoFlash()
		{
			Color original = _spriteRenderer.color;
			for (int c = 0; c < _flashTimes; c++)
			{
				_spriteRenderer.color = flashColor;
				yield return new WaitForSeconds(_flashColorTime);
				_spriteRenderer.color = original;
				//todo: skip this on last loop.
				yield return new WaitForSeconds(_flashOffTime);
			}
		}

		/// <summary>
		/// Settings for fade are serialized.
		/// </summary>
		public void FadeToColor()
		{
			StartCoroutine(DoFadeToColor());
		}
		public IEnumerator DoFadeToColor()
		{
			Color start = _spriteRenderer.color;
			Color end = fadeTargetColor;
			float t = 0;
			while (t<=1)
			{
				_spriteRenderer.color = Color.Lerp(start, end, t);
				t += Time.deltaTime / timeToFade;
				yield return null;
			}
		}
	}
}