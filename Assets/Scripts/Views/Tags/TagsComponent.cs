using UnityEngine;

namespace Views.Tags
{
	public class TagsComponent : MonoBehaviour
	{
		public GameObject[] Tags => _tags;
		
		[SerializeField] private GameObject[] _tags;
	}
}