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
		return new ChoppableObject[] { _choppableObjects[0] };
	}
}
