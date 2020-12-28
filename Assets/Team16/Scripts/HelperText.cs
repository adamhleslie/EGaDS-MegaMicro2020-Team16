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
		[SerializeField]
		private Animation _textAnimation;

		private int _currentIndex = -1;
		private bool _active;

		void Awake()
		{
			SetTextActiveInternal(false, true);
		}

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
			if (_active == active)
			{
				return;
			}

			SetTextActiveInternal(active, false);
			_active = active;
		}

		private void SetTextActiveInternal(bool active, bool instant)
		{
			if (active)
			{
				if (instant)
				{
					_text.transform.localScale = Vector3.one;
				}
				else
				{
					_textAnimation.Play("Text_PopIn");
				}
			}
			else
			{
				if (instant)
				{
					_text.transform.localScale = Vector3.zero;
				}
				else
				{
					_textAnimation.Play("Text_PopOut");
				}
			}
		}
	}
}
