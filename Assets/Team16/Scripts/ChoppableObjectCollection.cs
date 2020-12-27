using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChoppableObjectCollection", menuName = "Choppable Object Collection")]
public class ChoppableObjectCollection : ScriptableObject
{
	[SerializeField]
	private ChoppableObject[] _choppableObjects;

	// TODO: Need to make this randomized!
	public List<ChoppableObject> GetRandomUnique(int sizeRequested)
	{
		if (_choppableObjects.Length == 0) {
			return null;
        }
		
		List<ChoppableObject> listOfIndexes = new List<ChoppableObject>();
		
		for (int startIndex = 0; startIndex < sizeRequested; startIndex++)
        {
			listOfIndexes.Add(_choppableObjects[Random.Range(0, _choppableObjects.Length)]); 
        }

		return listOfIndexes;
	}
}
