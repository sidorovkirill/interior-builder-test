namespace InteriorBuilderTest.Core.StateMachine.Infrastructure
{
	public interface IState
	{
		void OnStateEntered();
		void OnStateExit();
	}
}