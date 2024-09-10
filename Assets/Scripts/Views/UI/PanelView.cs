using UnityEngine;

namespace Views.UI
{
	public abstract class PanelView : MonoBehaviour
	{
		public virtual void Show()
		{
			gameObject.SetActive(true);
		}

		public virtual void Hide()
		{
			gameObject.SetActive(false);
		}

		public virtual void Toggle(bool isActive)
		{
			gameObject.SetActive(isActive);
		}
	}
}