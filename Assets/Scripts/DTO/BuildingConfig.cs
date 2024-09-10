using System.Collections.Generic;
using UnityEngine;

namespace InteriorBuilderTest.DTO
{
	[CreateAssetMenu(fileName = "BuildingConfig", menuName = "ScriptableObjects/BuildingConfig", order = 1)]
	public class BuildingConfig : ScriptableObject
	{
		[SerializeField] public int MaxBuildingDistance;
		[SerializeField] public int UplacedObjectDistance;
	}
}