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
		private Image[] _choppableImages;
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
		private float _transitionExitingLength;
		[SerializeField]
		private float _transitionEnteringLength;
		[SerializeField]
		private float _chopTimerLength;
		[SerializeField]
		private float _postChopTimerLength;
		[SerializeField]
		private float _knifeFadeLength;
		[SerializeField]
		private Animation _choppableHolderAnimation;
		[SerializeField]
		private Animation _knifeAnimation;

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
				yield return new WaitForSeconds(_postChopTimerLength);
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
			if (_currentIndex != -1)
			{
				_choppableHolderAnimation.Play("ChoppableHolder_Leaving");
				yield return new WaitForSeconds(_transitionExitingLength);
			}

			if (Next())
			{
				_choppableHolderAnimation.Play("ChoppableHolder_Entering");
				yield return new WaitForSeconds(_transitionEnteringLength);
				StartPlay();
			}

			OnTransitionEnd();
		}

		private bool Next()
		{
			if (MinigameManager.Instance.minigame.gameWin == false)
			{
				Debug.Log($"Failed the minigame with index {_currentIndex}!");
				return false;
			}

			++_currentIndex;
			if (_currentIndex >= _usedObjects.Count)
			{
				Debug.Log($"Reached end of used objects with index {_currentIndex}");
				return false;
			}

			UpdateChoppable();
			return true;
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
			_knifeAnimation.CrossFade("Knife_Cut", _knifeFadeLength);
			_knifeAnimation.CrossFadeQueued("Knife_Idle", _knifeFadeLength);
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
		}

		private void StartPlay()
		{
			_waitingForInput = true;
			StartCoroutine(TimerCoroutine());
		}

		private void DisplayChoppableObject(ChoppableObject choppableObject, bool chopped)
		{
			foreach (Image image in _choppableImages)
			{
				if (chopped)
				{
					image.sprite = choppableObject.AfterChop;
				}
				else
				{
					image.sprite = choppableObject.BeforeChop;
				}
			}
		}
	}
}
