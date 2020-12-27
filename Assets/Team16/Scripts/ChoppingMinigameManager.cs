using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Team16
{
	public class ChoppingMinigameManager : MonoBehaviour
	{
		[SerializeField]
		private Image _choppableImage;
		[SerializeField]
		private Text _choppableText;
		[SerializeField]
		private Text _debugText;
		[SerializeField]
		private ChoppableObjectCollection _choppableObjects;
		[SerializeField]
		private int _numberOfChoppables;
		[SerializeField]
		private float _timerLength;
		[SerializeField]
		private float _transitionTimerLength;
		[SerializeField]
		private float _chopTimerLength;

		[SerializeField]
		private UnityEvent _onTransitionStart;
		[SerializeField]
		private UnityEvent _onTransitionEnd;
		[SerializeField]
		private UnityEvent _onChopStart;
		[SerializeField]
		private UnityEvent _onChopEnd;
		[SerializeField]
		private UnityEvent _onSuccess;
		[SerializeField]
		private UnityEvent _onFailure;

		private bool _waitingForInput;
		private List<ChoppableObject> _usedObjects;
		private int _currentIndex;

		void Start()
		{
			_usedObjects = _choppableObjects.GetRandomUnique(_numberOfChoppables);
			if ((_usedObjects == null) || (_usedObjects.Count == 0))
			{
				Debug.LogError("[ChoppingMinigameManager.Start] No choppable objects!");
				return;
			}

			foreach (ChoppableObject choppableObject in _usedObjects)
			{
				Debug.Log($"Choppable object: {choppableObject.name}");
			}

			MinigameManager.Instance.minigame.gameWin = true;
			_currentIndex = -1;
			StartCoroutine(TransitionCoroutine());
		}

		void Update()
		{
			if (_waitingForInput && Input.GetButtonDown("Space"))
			{
				_waitingForInput = false;

				// Stop the timer coroutine
				StopAllCoroutines();
				StartCoroutine(ChopCoroutine());
			}
		}

		private IEnumerator TimerCoroutine()
		{
			yield return new WaitForSeconds(_timerLength);

			_waitingForInput = false;
			if (!_usedObjects[_currentIndex].ShouldBeChopped)
			{
				OnSuccess();
				StartCoroutine(TransitionCoroutine());
			}
			else
			{
				OnFailure();
			}
		}

		private IEnumerator ChopCoroutine()
		{
			OnChopStart();
			yield return new WaitForSeconds(_chopTimerLength);
			OnChopEnd();

			if (_usedObjects[_currentIndex].ShouldBeChopped)
			{
				OnSuccess();
				StartCoroutine(TransitionCoroutine());
			}
			else
			{
				OnFailure();
			}
		}

		private IEnumerator TransitionCoroutine()
		{
			OnTransitionStart();
			yield return new WaitForSeconds(_transitionTimerLength);
			OnTransitionEnd();

			Next();
		}

		private void Next()
		{
			if (MinigameManager.Instance.minigame.gameWin == false)
			{
				Debug.Log($"Failed the minigame with index {_currentIndex}!");
				return;
			}

			++_currentIndex;
			if (_currentIndex >= _usedObjects.Count)
			{
				Debug.Log($"Reached end of used objects with index {_currentIndex}");
				return;
			}

			UpdateChoppable();
		}

		#region Visuals
		private void OnTransitionStart()
		{
			_debugText.text = "";
			_onTransitionStart?.Invoke();
		}

		private void OnTransitionEnd()
		{
			_debugText.text = "";
			_onTransitionEnd?.Invoke();
		}

		private void OnChopStart()
		{
			_debugText.text = "Chopping";
			_onChopStart?.Invoke();
		}

		private void OnChopEnd()
		{
			_onChopEnd?.Invoke();
			DisplayChoppableObject(_usedObjects[_currentIndex], true);
		}

		private void OnSuccess()
		{
			_debugText.text = "Success";
			_onSuccess?.Invoke();
		}

		private void OnFailure()
		{
			_debugText.text = "Failure";
			_onFailure?.Invoke();
			MinigameManager.Instance.minigame.gameWin = false;
		}
		#endregion

		private void UpdateChoppable()
		{
			DisplayChoppableObject(_usedObjects[_currentIndex], false);
			_choppableText.text = _usedObjects[_currentIndex].name;
			_waitingForInput = true;
			StartCoroutine(TimerCoroutine());
		}

		private void DisplayChoppableObject(ChoppableObject choppableObject, bool chopped)
		{
			if (chopped)
			{
				_choppableImage.sprite = choppableObject.AfterChop;
			}
			else
			{
				_choppableImage.sprite = choppableObject.BeforeChop;
			}
		}
	}
}
