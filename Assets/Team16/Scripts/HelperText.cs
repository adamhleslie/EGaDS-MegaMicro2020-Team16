using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Team16
{
	public class HelperText : MonoBehaviour
	{
		[SerializeField]
		private RectTransform[] _textHolders;
		[SerializeField]
		private Text _text;

		private int _currentIndex = -1;

		public void SetText(string text)
		{
			_text.text = text;
			if (_textHolders.Length == 0)
			{
				Debug.LogError("[HelperText.ShowHelperText] No text holders available");
				return;
			}

			int index;
			if (_textHolders.Length == 1)
			{
				index = 0;
			}
			else
			{
				do
				{
					index = Random.Range(0, _textHolders.Length);
				} while (_currentIndex == index);
			}

			_text.transform.SetParent(_textHolders[index], false);
			_currentIndex = index;
		}

		public void SetTextActive(bool active)
		{
			_text.gameObject.SetActive(active);
		}
	}
}
