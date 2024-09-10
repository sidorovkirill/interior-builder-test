using System.Collections.Generic;
using UnityEngine;

namespace InteriorBuilderTest.DTO
{
	[CreateAssetMenu(fileName = "AvailableAssets", menuName = "ScriptableObjects/AvailableAssets", order = 1)]
	public class AvailableAssets : ScriptableObject
	{
		[SerializeField] public List<PlaceableAsset> Assets;
	}
}