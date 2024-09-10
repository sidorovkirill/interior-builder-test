using UnityEngine;
using Views;

namespace Components
{
	public struct Furniture
	{
		public GameObject Obj { get; set; }
		public GameObject[] AllowedTags { get; set; }
		public NewFurnitureObjView View { get; set; }
		public Quaternion Rotation { get; set; }
	}
}