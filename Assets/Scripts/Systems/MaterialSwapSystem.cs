using System.Linq;
using Components;
using InteriorBuilderTest.Components;
using InteriorBuilderTest.DTO;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
	public class MaterialSwapSystem : IEcsRunSystem
	{
		private readonly MaterialConfig _materialConfig;
		private EcsPoolInject<Furniture> _furniturePool;
		private EcsPoolInject<NewObject> _newObjectPool;
		private EcsFilterInject<Inc<Furniture, NewObject>> _newFurnitureFilter;

		public MaterialSwapSystem(MaterialConfig materialConfig)
		{
			_materialConfig = materialConfig;
		}

		public void Run(IEcsSystems systems)
		{
			foreach (var newFurnitureEntity in _newFurnitureFilter.Value)
			{
				ref var furniture = ref _furniturePool.Value.Get(newFurnitureEntity);
				ref var newObject = ref _newObjectPool.Value.Get(newFurnitureEntity);

				if (!newObject.IsPlaced)
				{
					furniture.View.UpdateMaterial(_materialConfig.UnplacedMaterial);
				}
				else if (newObject.IsPlaced && newObject.IsAllowedSurface && !furniture.View.IsColliding)
				{
					furniture.View.UpdateMaterial(_materialConfig.RightPlacementMaterial);
				}
				else
				{
					furniture.View.UpdateMaterial(_materialConfig.BadPlacementMaterial);
				}
			}
		}
	}
}