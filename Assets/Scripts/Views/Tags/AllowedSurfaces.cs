using UnityEngine;

namespace Views.Tags
{
	public class AllowedSurfaces : MonoBehaviour
	{
		public GameObject[] Tags => _tags;
		
		[SerializeField] private GameObject[] _tags;
	}
}