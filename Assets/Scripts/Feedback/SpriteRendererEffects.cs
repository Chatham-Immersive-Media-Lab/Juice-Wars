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
		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

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
	}
}