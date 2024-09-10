using System;
using System.Collections.Generic;
using Components;
using InteriorBuilderTest.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Views;
using Views.Tags;

namespace InteriorBuilderTest.Systems.Input
{
	public class RaycastSystem : IEcsInitSystem, IEcsDestroySystem
	{
		private RaycastView _raycastView;
		private EcsPoolInject<Hit> _raycastHitPool;
		private EcsPoolInject<Miss> _raycastMissPool;
		private EcsWorldInject _world;
		private GameObject _lastHitObject;
		private GameObject[] _tags;

		public RaycastSystem(RaycastView raycastView)
		{
			_raycastView = raycastView;
		}
		
		public void Init(IEcsSystems systems)
		{
			_raycastView.HitRegistered += HandleRaycastHit;
			_raycastView.MissRegistered += HandleRaycastMiss;
		}

		private void HandleRaycastMiss(object sender, Miss missData)
		{
			var hitEntity = _world.Value.NewEntity();
			ref var miss = ref _raycastMissPool.Value.Add(hitEntity);
			miss = missData;
		}

		private void HandleRaycastHit(object sender, RaycastHit hit)
		{
			var hitEntity = _world.Value.NewEntity();
			ref var raycast = ref _raycastHitPool.Value.Add(hitEntity);
			raycast.RaycastHit = hit;

			var hitObject = hit.collider.gameObject;
			if (hitObject != _lastHitObject)
			{
				var tagsComponent = hitObject.GetComponent<TagsComponent>();
				_tags = tagsComponent?.Tags;
				_lastHitObject = hitObject;
			}
			
			raycast.Tags = _tags;
		}

		public void Destroy(IEcsSystems systems)
		{
			_raycastView.HitRegistered -= HandleRaycastHit;
		}
	}
}