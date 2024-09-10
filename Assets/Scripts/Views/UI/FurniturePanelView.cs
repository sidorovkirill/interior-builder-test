using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.UI
{
	public class FurniturePanelView : PanelView
	{
		public event EventHandler<int> ItemClicked;
		public event EventHandler PanelClosed;
		
		[SerializeField] private GameObject _itemPrefab;
		[SerializeField] private GameObject _itemsRoot;
		[SerializeField] private Button _closeButton;
		private List<FurnitureItemView> _items = new ();

		private void Start()
		{
			_closeButton.onClick.AddListener(HandleCloseButtonClick);
		}

		private void HandleCloseButtonClick()
		{
			PanelClosed?.Invoke(this, EventArgs.Empty);
		}

		public void CreateItem(int id, Sprite sprite)
		{
			var item = Instantiate(_itemPrefab, _itemsRoot.transform, true);
			var view = item.GetComponent<FurnitureItemView>();
			view.Setup(id, sprite);
			view.Clicked += HandleClick;
			_items.Add(view);
		}

		private void HandleClick(object sender, int id)
		{
			ItemClicked?.Invoke(this, id);
		}

		private void OnDestroy()
		{
			foreach (var item in _items)
			{
				item.Clicked -= HandleClick;
			}
			_closeButton.onClick.RemoveListener(HandleCloseButtonClick);
		}
	}
}