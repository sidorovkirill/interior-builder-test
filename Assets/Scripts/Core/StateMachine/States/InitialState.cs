using InteriorBuilderTest.Core.StateMachine.Infrastructure;

namespace InteriorBuilderTest.Core.StateMachine.States
{
	public class InitialState : IState
	{
		private readonly StateRequester _stateRequester;

		public InitialState(
			StateRequester stateRequester
			)
		{
			_stateRequester = stateRequester;
		}
		
		public void OnStateEntered()
		{
			_stateRequester.GoToState(StateTrigger.Main);
		}

		public void OnStateExit()
		{
		}
	}
}