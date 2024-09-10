using System;
using UnityEngine;

namespace Views
{
	public class NewFurnitureObjView : MonoBehaviour
	{
		public bool IsColliding => _colliderView.IsColliding;
		
		// Colliders that will use after furniture placing
		[SerializeField] private Collider[] _notIgnoringColliders;
		[SerializeField] private Renderer[] _objectsWithMaterial;
		[SerializeField] private Material _originalMaterial;
		[SerializeField] private FurnitureColliderView _colliderView;

		public void ToggleColliders(bool enable)
		{
			foreach (var collider in _notIgnoringColliders)
			{
				collider.enabled = enable;
			}
		}
		
		public void UpdateMaterial(Material material)
		{
			foreach (var renderer in _objectsWithMaterial)
			{
				renderer.material = material;
			}
		}

		public void RestoreMaterial()
		{
			UpdateMaterial(_originalMaterial);
		}
	}
}