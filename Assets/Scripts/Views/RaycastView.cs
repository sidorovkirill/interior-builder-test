using System;
using InteriorBuilderTest.Components;
using InteriorBuilderTest.DTO;
using UnityEngine;
using Zenject;

namespace Views
{
	public class RaycastView : MonoBehaviour
	{
		public event EventHandler<RaycastHit> HitRegistered;
		public event EventHandler<Miss> MissRegistered;

		private const int RaycastIgnoreLevel = 2;
		private readonly int _layerMask = ~(1 << RaycastIgnoreLevel);

		[SerializeField] private Camera _playerCamera;
		[Inject]
		private BuildingConfig _buildingConfig;

		void FixedUpdate()
		{
			RaycastHit hit;
			if (Physics.Raycast(
				    _playerCamera.transform.position, 
				    _playerCamera.transform.forward, 
				    out hit, 
				    _buildingConfig.MaxBuildingDistance,
				    _layerMask)
			    )
			{
				HitRegistered?.Invoke(this, hit);
			}
			else
			{
				var miss = new Miss();
				miss.Position = _playerCamera.transform.TransformDirection(Vector3.forward) *
					_buildingConfig.UplacedObjectDistance + _playerCamera.transform.position;
				miss.Rotation = _playerCamera.transform.rotation;
				MissRegistered?.Invoke(this, miss);
			}
		}
	}
}