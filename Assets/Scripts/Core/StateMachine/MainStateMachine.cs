using Stateless;
using InteriorBuilderTest.Core.StateMachine.Infrastructure;
using InteriorBuilderTest.Core.StateMachine.States;
using Zenject;

namespace InteriorBuilderTest.Core.StateMachine
{
	public class MainStateMachine : IMainStateMachine, IInitializable
	{
		private readonly StateRequester _stateRequester;
		private readonly InitialState _initialState;
		private readonly MainState _mainState;

		private StateMachine<IState, StateTrigger> _machine;

		public MainStateMachine(
			StateRequester stateRequester,
			InitialState initialState,
			MainState mainState
		)
		{
			_stateRequester = stateRequester;
			_initialState = initialState;
			_mainState = mainState;
		}
		
		public void Initialize()
		{
			_stateRequester.OnRequestNewState += ChangeState;
			_machine = new StateMachine<IState, StateTrigger>(_initialState);

			ConfigureTransitions();
			ConfigureStates();
			_machine.Fire(StateTrigger.Main);
		}

		public void ChangeState(object sender, StateTrigger stateTrigger)
		{
			_machine.Fire(stateTrigger);
		}

		private void ConfigureTransitions()
		{
			_machine.Configure(_initialState)
				.Permit(StateTrigger.Main, _mainState);
		}

		private void ConfigureStates()
		{
			ConfigureState(_initialState);
			ConfigureState(_mainState);
		}
		
		private void ConfigureState(IState client)
		{
			_machine.Configure(client)
				.OnEntry(client.OnStateEntered)
				.OnExit(client.OnStateExit);
		}
	}
}