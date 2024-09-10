using System;

namespace InteriorBuilderTest.Core.StateMachine
{
	public class StateRequester
	{
		public StateTrigger CurrentState { get; private set; }
		public StateTrigger LastState { get; private set; }
		public event EventHandler<StateTrigger> OnRequestNewState;

		public void GoToState(StateTrigger state)
		{
			OnRequestNewState?.Invoke(this, state);
			LastState = CurrentState;
			CurrentState = state;
		}
	}
}