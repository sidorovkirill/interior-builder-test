using Components;
using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Views;
using Views.Tags;

namespace Systems
{
	public class FurnitureBuilderSystem : IEcsRunSystem
	{
		private FurnitureBuilderView _furnitureBuilderView;
		private EcsPoolInject<FurnitureCreated> _createdEventPool;
		private EcsPoolInject<Furniture> _furniturePool;
		private EcsPoolInject<NewObject> _newObjectPool;
		private EcsFilterInject<Inc<FurnitureCreated>> _createdEventFilter;
		private EcsWorldInject _world;

		public FurnitureBuilderSystem(FurnitureBuilderView furnitureBuilderView)
		{
			_furnitureBuilderView = furnitureBuilderView;
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var eventEntity in _createdEventFilter.Value)
			{
				ref var createEvent = ref _createdEventPool.Value.Get(eventEntity);
				var newFurnitureObj = _furnitureBuilderView.Create(createEvent.Prefab);
				var allowedTags = newFurnitureObj.GetComponent<AllowedSurfaces>();
				var furnitureView = newFurnitureObj.GetComponent<NewFurnitureObjView>();
				furnitureView.ToggleColliders(false);

				var newFurnitureEntity = _world.Value.NewEntity();
				ref var furniture = ref _furniturePool.Value.Add(newFurnitureEntity);
				furniture.Obj = newFurnitureObj;
				furniture.AllowedTags = allowedTags.Tags;
				furniture.View = furnitureView;
				furniture.Rotation = Quaternion.identity;
				
				ref var newObject = ref _newObjectPool.Value.Add(newFurnitureEntity);
				newObject.IsPlaced = false;
				newObject.IsAllowedSurface = false;
				newObject.IsNotColliding = false;
			}
		}
	}
}