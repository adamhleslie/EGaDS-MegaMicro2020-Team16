﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team16
{
	[CreateAssetMenu(fileName = "New ChoppableObject", menuName = "Choppable Object")]
	public class ChoppableObject : ScriptableObject
	{
		public bool ShouldBeChopped { get { return _shouldBeChopped; } }
		public Sprite BeforeChop { get { return _beforeChop; } }
		public Sprite AfterChop { get { return _afterChop; } }
		public string CustomLoseSound { get { return _customLoseSound; } }
		public string CustomLoseText { get { return _customLoseText; } }

		[SerializeField]
		private bool _shouldBeChopped;
		[SerializeField]
		private Sprite _beforeChop;
		[SerializeField]
		private Sprite _afterChop;
		[SerializeField]
		private string[] _helperText;
		[SerializeField]
		private string _customLoseSound;
		[SerializeField]
		private string _customLoseText;

		public string GetRandomHelperText()
		{
			if (_helperText.Length == 0)
			{
				return null;
			}

			return _helperText[Random.Range(0, _helperText.Length)];
		}
	}
}
