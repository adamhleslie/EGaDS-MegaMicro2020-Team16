using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChoppableObject", menuName = "Choppable Object")]
public class ChoppableObject : ScriptableObject
{
	[SerializeField]
	private bool _shouldBeChopped;

	[SerializeField]
	private Texture2D _beforeChop;

	[SerializeField]
	private Texture2D _afterChop;
}
