using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChoppableObjectCollection", menuName = "Choppable Object Collection")]
public class ChoppableObjectCollection : ScriptableObject
{
	[SerializeField]
	private ChoppableObject[] _choppableObjects;

	// TODO: Need to make this randomized!
	public ChoppableObject[] GetRandomUnique()
	{
		if (_choppableObjects.Length == 0) {
			return null;
        }

		int randomIndex = Random.Range(0, _choppableObjects.Length);
		return new ChoppableObject[] { _choppableObjects[randomIndex] };
	}
}
