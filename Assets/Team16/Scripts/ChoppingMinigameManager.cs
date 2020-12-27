using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Team16
{
	public class ChoppingMinigameManager : MonoBehaviour
	{
		[SerializeField]
		private ChoppableObjectCollection _choppableObjects;
		[SerializeField]
		private float _timerLength;
		[SerializeField]
		private float _transitionTimerLength;
		[SerializeField]
		private Image _choppableImage;

		private bool _waitingForInput;
		private ChoppableObject[] _usedObjects;
		private int _currentIndex;

		void Start()
		{
			_usedObjects = _choppableObjects.GetRandomUnique();
			if ((_usedObjects == null) || (_usedObjects.Length == 0))
			{
				Debug.LogError("[ChoppingMinigameManager.Start] No choppable objects!");
				return;
			}

			foreach (ChoppableObject choppableObject in _usedObjects)
			{
				Debug.Log($"Choppable object: {choppableObject.name}");
			}

			_currentIndex = 0;
			DisplayChoppableObject(_usedObjects[_currentIndex], false);
			_waitingForInput = true;
			StartCoroutine(TimerCoroutine());
		}

		void Update()
		{
			if (_waitingForInput && Input.GetButtonDown("Space"))
			{
				_waitingForInput = false;
				if (_usedObjects[_currentIndex].ShouldBeChopped)
				{
					// Handle success!
					MinigameManager.Instance.minigame.gameWin = true;
				}
				else
				{
					// Handle failure!
					MinigameManager.Instance.minigame.gameWin = false;
				}

				// Stop the timer
				StopAllCoroutines();
				DisplayChoppableObject(_usedObjects[_currentIndex], true);
				StartCoroutine(TransitionTimerCoroutine());
			}
		}

		private IEnumerator TimerCoroutine()
		{
			yield return new WaitForSeconds(_timerLength);

			_waitingForInput = false;
			if (!_usedObjects[_currentIndex].ShouldBeChopped)
			{
				// Handle success!
				MinigameManager.Instance.minigame.gameWin = true;
			}
			else
			{
				// Handle failure!
				MinigameManager.Instance.minigame.gameWin = false;
			}

			StartCoroutine(TransitionTimerCoroutine());
		}

		// TODO: Update to show effects
		private IEnumerator TransitionTimerCoroutine()
		{
			yield return new WaitForSeconds(_transitionTimerLength);
			Next();
		}

		private void Next()
		{
			// Add support for multiple objects
			++_currentIndex;
			if (_currentIndex >= _usedObjects.Length)
			{
				Debug.Log("Reached end of used objects");
				return;
			}

			/// TODO: Visuals of cutting board sliding in etc.
			DisplayChoppableObject(_usedObjects[_currentIndex], false);
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
