using System;
using System.Collections.Generic;
using UnityEngine;

namespace Team16
{
	[CreateAssetMenu(fileName = "New ChoppableObjectCollection", menuName = "Choppable Object Collection")]
	public class ChoppableObjectCollection : ScriptableObject
	{
		[Serializable]
		private class ChoppableObjectInternal
		{
			public ChoppableObject ChoppableObject;
			public float Probability;
		}

		[SerializeField]
		private ChoppableObjectInternal[] _choppableObjects;

		public List<ChoppableObject> GetRandom(int sizeRequested)
		{
			if (_choppableObjects.Length == 0)
			{
				return null;
			}

			List<ChoppableObject> choppables = new List<ChoppableObject>();
			for (int startIndex = 0; startIndex < sizeRequested; startIndex++)
			{
				choppables.Add(GetRandom());;
			}

			return choppables;
		}

		private ChoppableObject GetRandom()
		{
			float max = 0;
			foreach (ChoppableObjectInternal choppableObject in _choppableObjects)
			{
				max += choppableObject.Probability;
			}

			float currentMax = 0;
			float value = UnityEngine.Random.Range(0, max);
			foreach (ChoppableObjectInternal choppableObject in _choppableObjects)
			{
				currentMax += choppableObject.Probability;
				if (value <= currentMax)
				{
					return choppableObject.ChoppableObject;
				}
			}

			return null;
		}
	}
}
