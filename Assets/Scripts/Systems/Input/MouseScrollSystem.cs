using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Views;

namespace InteriorBuilderTest.Systems.Input
{
	public class MouseScrollSystem : IEcsInitSystem, IEcsDestroySystem
	{
		private readonly ButtonInputView _buttonInputView;
		private readonly EcsWorldInject _world;
		private readonly EcsPoolInject<MouseScrolled> _mouseScrolledEventPool;

		public MouseScrollSystem(ButtonInputView buttonInputView)
		{
			_buttonInputView = buttonInputView;
		}
		
		public void Init(IEcsSystems systems)
		{
			_buttonInputView.Scrolled += HandleScroll;
		}

		private void HandleScroll(object sender, float delta)
		{
			var entity = _world.Value.NewEntity();
			ref var scrollEvent = ref _mouseScrolledEventPool.Value.Add(entity);

			scrollEvent.Delta = delta;
		}

		public void Destroy(IEcsSystems systems)
		{
			_buttonInputView.Scrolled -= HandleScroll;
		}
	}
}