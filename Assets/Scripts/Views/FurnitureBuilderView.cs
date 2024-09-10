using UnityEngine;
using UnityEngine.Pool;

namespace Views
{
	public class FurnitureBuilderView : MonoBehaviour
	{
		[SerializeField] private GameObject _furnitureRoot;
		private ObjectPool<GameObject> _pool;

		public GameObject Create(GameObject prefab)
		{
			return Instantiate(prefab, _furnitureRoot.transform, true);
		}
	}
}