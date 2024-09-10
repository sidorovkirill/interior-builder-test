using System;
using Constants;
using UnityEngine;

namespace Views
{
	public class ButtonInputView : MonoBehaviour
	{
		public event EventHandler<ButtonAction> ButtonClicked;
		public event EventHandler<float> Scrolled;

		private const string FurnitureButtonName = "Furniture Panel";
		private const string ClosePanelButtonName = "Cancel";
		private const string LeftMouseButtonName = "Fire1";
		private const string ScrollName = "Mouse ScrollWheel";

		void Update()
		{
			if (Input.GetButtonDown(FurnitureButtonName))
			{
				ButtonClicked?.Invoke(this, ButtonAction.OpenFurniturePanel);
			}
			if (Input.GetButtonDown(ClosePanelButtonName))
			{
				ButtonClicked?.Invoke(this, ButtonAction.ClosePanel);
			}
			if (Input.GetButtonDown(LeftMouseButtonName))
			{
				ButtonClicked?.Invoke(this, ButtonAction.Drop);
			}
			
			float scrollWheelChange = Input.GetAxis(ScrollName);
			if (Mathf.Abs(scrollWheelChange) > 0)
			{
				Scrolled?.Invoke(this, scrollWheelChange);
			}
		}
	}
}