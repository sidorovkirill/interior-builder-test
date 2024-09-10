using System;
using UnityEngine;

namespace InteriorBuilderTest.DTO
{
	[Serializable]
	public struct PlaceableAsset
	{
		public Sprite Icon;
		public GameObject Prefab;
	}
}