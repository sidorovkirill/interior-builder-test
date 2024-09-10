using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.UI
{
	public class FurnitureItemView : MonoBehaviour
	{
		public event EventHandler<int> Clicked;

		private int _id;
		private Button _button;

		public void Setup(int id, Sprite sprite)
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(HandleClick);
			
			_id = id;
			_button.image.sprite = sprite;
		}

		private void HandleClick()
		{
			Clicked?.Invoke(this, _id);
		}

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(HandleClick);
		}
	}
}