using System;
using InteriorBuilderTest.Components;
using InteriorBuilderTest.DTO;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Views.UI;

namespace InteriorBuilderTest.Systems.UI
{
	public class FurniturePanelSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
	{
		private readonly FurniturePanelView _furniturePanelView;
		private readonly AvailableAssets _availableAssets;
		private readonly EcsPoolInject<FurniturePanelToggled> _furniturePanelToggledEventPool;
		private readonly EcsPoolInject<FurnitureCreated> _furnitureCreatedEventPool;
		private readonly EcsFilterInject<Inc<FurniturePanelToggled>> _furniturePanelToggledEventFilter;
		private readonly EcsFilterInject<Inc<PanelClosed>> _panelClosedEventFilter;
		private readonly EcsWorldInject _world;

		public FurniturePanelSystem(FurniturePanelView furniturePanelView, AvailableAssets availableAssets)
		{
			_furniturePanelView = furniturePanelView;
			_availableAssets = availableAssets;
		}
		
		public void Init(IEcsSystems systems)
		{
			PopulatePanel();
			_furniturePanelView.ItemClicked += HandleItemClick;
			_furniturePanelView.PanelClosed += HandlePanelClose;
		}

		private void HandlePanelClose(object sender, EventArgs e)
		{
			FireHidePanelEvent();
		}

		private void HandleItemClick(object sender, int id)
		{
			FireCreatedEvent(id);
			FireHidePanelEvent();
		}
		
		private void FireCreatedEvent(int id)
		{
			var eventEntity = _world.Value.NewEntity();
			ref var furniture = ref _furnitureCreatedEventPool.Value.Add(eventEntity);
			furniture.Prefab = _availableAssets.Assets[id].Prefab;
		}

		private void FireHidePanelEvent()
		{
			var eventEntity = _world.Value.NewEntity();
			ref var toggleEvent = ref _furniturePanelToggledEventPool.Value.Add(eventEntity);
			toggleEvent.IsActive = false;
		}

		private void PopulatePanel()
		{
			for (int i = 0; i < _availableAssets.Assets.Count; i++)
			{
				var asset = _availableAssets.Assets[i];
				_furniturePanelView.CreateItem(i, asset.Icon);
			}
		}

		public void Run(IEcsSystems systems)
		{
			CheckForToggleEvent();
			CheckForCloseEvent();
		}

		private void CheckForToggleEvent()
		{
			foreach (var eventEntity in _furniturePanelToggledEventFilter.Value)
			{
				ref var toggleEvent = ref _furniturePanelToggledEventPool.Value.Get(eventEntity);
				_furniturePanelView.Toggle(toggleEvent.IsActive);
			}
		}

		private void CheckForCloseEvent()
		{
			if(_panelClosedEventFilter.Value.GetEntitiesCount() > 0)
			{
				_furniturePanelView.Hide();
			}
		}
		
		public void Destroy(IEcsSystems systems)
		{
			_furniturePanelView.ItemClicked -= HandleItemClick;
			_furniturePanelView.PanelClosed -= HandlePanelClose;
		}
	}
}