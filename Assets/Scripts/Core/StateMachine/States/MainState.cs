using InteriorBuilderTest.Core.StateMachine.Infrastructure;
using Views.UI;

namespace InteriorBuilderTest.Core.StateMachine.States
{
    public class MainState : IState
    {
	    private readonly FurniturePanelView _furniturePanelView;

		public MainState(FurniturePanelView furniturePanelView)
		{
			_furniturePanelView = furniturePanelView;
		}

        public void OnStateEntered()
        {
	        HideAllPanels();
        }

		public void OnStateExit()
		{
			
		}

		private void HideAllPanels()
		{
			_furniturePanelView.Hide();
		}
	}
}