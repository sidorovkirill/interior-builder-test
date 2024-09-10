using Components;
using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
	public class FurnitureRotationSystem : IEcsRunSystem
	{
		private const float ScrollMultiplier = 100;
		private readonly EcsPoolInject<MouseScrolled> _mouseScrolledEventPool;
		private readonly EcsFilterInject<Inc<MouseScrolled>> _mouseScrolledEventFilter;
		private readonly EcsPoolInject<NewObject> _newObjectPool;
		private readonly EcsPoolInject<Furniture> _furniturePool;
		private readonly EcsFilterInject<Inc<Furniture, NewObject>> _newFurnitureFilter;

		public void Run(IEcsSystems systems)
		{
			foreach (var scrollEntity in _mouseScrolledEventFilter.Value)
			{
				ref var scrollEvent = ref _mouseScrolledEventPool.Value.Get(scrollEntity);
				foreach (var newFurnitureEntity in _newFurnitureFilter.Value)
				{
					ref var furniture = ref _furniturePool.Value.Get(newFurnitureEntity);
					var angle = scrollEvent.Delta * ScrollMultiplier;
					furniture.Rotation *= Quaternion.Euler(0,angle, 0);
				}
			}
		}
	}
}