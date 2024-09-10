using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace InteriorBuilderTest.Systems.UI
{
	public class CursorControlSystem : IEcsInitSystem, IEcsRunSystem
	{
		private readonly FirstPersonController _firstPersonController;
		private readonly EcsPoolInject<FurniturePanelToggled> _furniturePanelToggledEventPool;
		private readonly EcsFilterInject<Inc<FurniturePanelToggled>> _furniturePanelToggledEventFilter;
		private readonly EcsFilterInject<Inc<PanelClosed>> _panelClosedEventFilter;
		private readonly EcsWorldInject _world;

		public CursorControlSystem(FirstPersonController firstPersonController)
		{
			_firstPersonController = firstPersonController;
		}
		
		public void Init(IEcsSystems systems)
		{
			_firstPersonController.lockCursor = true;
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
				if (toggleEvent.IsActive)
				{
					_firstPersonController.UnlockCursor();
				}
				else
				{
					_firstPersonController.LockCursor();
				}

				_firstPersonController.cameraCanMove = !toggleEvent.IsActive;
			}
		}

		private void CheckForCloseEvent()
		{
			if(_panelClosedEventFilter.Value.GetEntitiesCount() > 0)
			{
				_firstPersonController.LockCursor();
				_firstPersonController.cameraCanMove = true;
			}
		}
	}
}