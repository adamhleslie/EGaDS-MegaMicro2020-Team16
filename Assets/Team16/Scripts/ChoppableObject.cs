using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChoppableObject", menuName = "Choppable Object")]
public class ChoppableObject : ScriptableObject
{
	public bool ShouldBeChopped { get { return _shouldBeChopped; } }
	public Sprite BeforeChop { get { return _beforeChop; } }
	public Sprite AfterChop { get { return _afterChop; } }

	[SerializeField]
	private bool _shouldBeChopped;

	[SerializeField]
	private Sprite _beforeChop;

	[SerializeField]
	private Sprite _afterChop;
}
