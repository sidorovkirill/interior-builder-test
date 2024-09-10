using System.Linq;
using Components;
using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
	public class PositioningNewObjSystem : IEcsRunSystem
	{
		private EcsPoolInject<Furniture> _furniturePool;
		private EcsPoolInject<NewObject> _newObjectPool;
		private EcsPoolInject<Hit> _hitPool;
		private EcsPoolInject<Miss> _missPool;
		private EcsFilterInject<Inc<Furniture, NewObject>> _newFurnitureFilter;
		private EcsFilterInject<Inc<ObjectDropped>> _objectDroppedEventFilter;
		private EcsFilterInject<Inc<Hit>> _hitFilter;
		private EcsFilterInject<Inc<Miss>> _missFilter;

		public void Run(IEcsSystems systems)
		{
			PlaceOnHit();
			PlaceOnMiss();
		}

		private void PlaceOnHit()
		{
			foreach (var hitEntity in _hitFilter.Value)
			{
				ref var hit = ref _hitPool.Value.Get(hitEntity);
				foreach (var newFurnitureEntity in _newFurnitureFilter.Value)
				{
					ref var furniture = ref _furniturePool.Value.Get(newFurnitureEntity);
					furniture.Obj.transform.position = hit.RaycastHit.point;
					furniture.Obj.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.RaycastHit.normal) * furniture.Rotation;
					
					ref var newObject = ref _newObjectPool.Value.Get(newFurnitureEntity);
					newObject.IsAllowedSurface = IsAllowedSurface(hit.Tags, furniture.AllowedTags);
					newObject.IsPlaced = true;
				}
			}
		}

		private void PlaceOnMiss()
		{
			foreach (var missEntity in _missFilter.Value)
			{
				ref var miss = ref _missPool.Value.Get(missEntity);
				foreach (var newFurnitureEntity in _newFurnitureFilter.Value)
				{
					ref var furniture = ref _furniturePool.Value.Get(newFurnitureEntity);
					furniture.Obj.transform.position = miss.Position;
					furniture.Obj.transform.rotation = miss.Rotation * furniture.Rotation;
					
					ref var newObject = ref _newObjectPool.Value.Get(newFurnitureEntity);
					newObject.IsAllowedSurface = false;
					newObject.IsPlaced = false;
				}
			}
		}

		private bool IsAllowedSurface(GameObject[] hitTags, GameObject[] allowedTags)
		{
			return hitTags != null &&
			       allowedTags != null &&
			       hitTags.Length > 0 &&
			       allowedTags.Length > 0 &&
			       hitTags.Intersect(allowedTags).Any();
		}
	}
}