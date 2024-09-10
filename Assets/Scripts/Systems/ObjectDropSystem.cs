using Components;
using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class ObjectDropSystem : IEcsRunSystem
	{
		private readonly EcsPoolInject<NewObject> _newObjectPool;
		private readonly EcsFilterInject<Inc<Furniture, NewObject>> _newFurnitureFilter;
		private readonly EcsPoolInject<Furniture> _furniturePool;
		private readonly EcsFilterInject<Inc<ObjectDropped>> _objectDroppedEventFilter;
		
		public void Run(IEcsSystems systems)
		{
			if (_objectDroppedEventFilter.Value.GetEntitiesCount() > 0)
			{
				foreach (var newFurnitureEntity in _newFurnitureFilter.Value)
				{
					ref var furniture = ref _furniturePool.Value.Get(newFurnitureEntity);
					ref var newObject = ref _newObjectPool.Value.Get(newFurnitureEntity);

					if (newObject.IsPlaced && newObject.IsAllowedSurface && !furniture.View.IsColliding)
					{
						furniture.View.RestoreMaterial();
						furniture.View.ToggleColliders(true);
						_newObjectPool.Value.Del(newFurnitureEntity);
					}
				}
			}
		}
	}
}