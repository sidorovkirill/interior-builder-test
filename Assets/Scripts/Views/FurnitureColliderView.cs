using UnityEngine;

namespace Views
{
	public class FurnitureColliderView : MonoBehaviour
	{
		public bool IsColliding { get; private set; }
		private void OnTriggerEnter(Collider other)
		{
			IsColliding = true;
		}

		private void OnTriggerExit(Collider other)
		{
			IsColliding = false;
		}
	}
}