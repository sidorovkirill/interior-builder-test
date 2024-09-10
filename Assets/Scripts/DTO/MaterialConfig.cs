using System.Collections.Generic;
using UnityEngine;

namespace InteriorBuilderTest.DTO
{
	[CreateAssetMenu(fileName = "MaterialConfig", menuName = "ScriptableObjects/MaterialConfig", order = 1)]
	public class MaterialConfig : ScriptableObject
	{
		[SerializeField] public Material UnplacedMaterial;
		[SerializeField] public Material BadPlacementMaterial;
		[SerializeField] public Material RightPlacementMaterial;
	}
}