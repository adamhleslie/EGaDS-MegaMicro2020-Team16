using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Team16
{
    public class TimerVisual : MonoBehaviour
    {
        [SerializeField]
        private Image _radialFill;
		[SerializeField]
		private UnityEvent _onComplete;

		private float _length;
		private float _currentTime;
		private bool _active;
		private Action _completeCallback;

		public void StartTimer(float length, Action completeCallback)
		{
			_length = length;
			_currentTime = 0f;
			_active = true;
			_completeCallback = completeCallback;
		}

		public void StopTimer()
		{
			_active = false;
		}

		public void ClearFill()
		{
			if (_active)
			{
				Debug.LogError("[TimerVisual.ClearFill] Attempting to clear fill while timer is active");
				return;
			}

			SetFill(0f);
		}

		void Update()
		{
			if (_active)
			{
				_currentTime += Time.deltaTime;

				float fillValue;
				if (_currentTime >= _length)
				{
					fillValue = 1.0f;
					_active = false;
					_onComplete?.Invoke();
					_completeCallback?.Invoke();
				}
				else
				{
					fillValue = _currentTime / _length;
				}

				SetFill(fillValue);
			}
		}

		private void SetFill(float value)
		{
			_radialFill.fillAmount = value;
		}
	}
}
