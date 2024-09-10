using Constants;
using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Views;

namespace InteriorBuilderTest.Systems.Input
{
	public class ButtonsInputSystem : IEcsInitSystem, IEcsDestroySystem
	{
		private readonly ButtonInputView _buttonInputView;
		private readonly EcsPoolInject<FurniturePanelToggled> _furniturePanelToggledEventPool;
		private readonly EcsWorldInject _world;

		public ButtonsInputSystem(ButtonInputView buttonInputView)
		{
			_buttonInputView = buttonInputView;
		}
		
		public void Init(IEcsSystems systems)
		{
			_buttonInputView.ButtonClicked += FireButtonEvent;
		}

		private void FireButtonEvent(object sender, ButtonAction buttonAction)
		{
			switch (buttonAction)
			{
				case ButtonAction.OpenFurniturePanel:
					ref var toggleEvent = ref CreateComponent<FurniturePanelToggled>();
					toggleEvent.IsActive = true;
					break;
				case ButtonAction.ClosePanel:
					CreateComponent<PanelClosed>();
					break;
				case ButtonAction.Drop:
					CreateComponent<ObjectDropped>();
					break;
			}
		}

		private ref T CreateComponent<T>() where T : struct
		{
			var eventEntity = _world.Value.NewEntity();
			var pool = _world.Value.GetPool<T>();
			ref var buttonEvent = ref pool.Add(eventEntity);
			return ref buttonEvent;
		}

		public void Destroy(IEcsSystems systems)
		{
			_buttonInputView.ButtonClicked -= FireButtonEvent;
		}
		
		
	}
}