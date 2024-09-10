using UnityEngine;

namespace InteriorBuilderTest.Components
{
	public struct Hit
	{
		public RaycastHit RaycastHit { get; set; }
		public GameObject[] Tags { get; set; }
	}
}