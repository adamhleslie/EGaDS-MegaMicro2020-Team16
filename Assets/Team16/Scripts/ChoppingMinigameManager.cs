using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team16
{
    public class ChoppingMinigameManager : MonoBehaviour
    {
		[SerializeField]
		private ChoppableObjectCollection _choppableObjects;
		[SerializeField]
		private float _timerLength;

		private ChoppableObject[] _usedObjects;

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

			StartCoroutine(TimerCoroutine());
		}

		private IEnumerator TimerCoroutine()
		{
			yield return new WaitForSeconds(_timerLength);
			Debug.Log($"{_timerLength}s have passed");
		}
	}
}
