namespace InteriorBuilderTest.Core.StateMachine.Infrastructure
{
	public interface IMainStateMachine
	{
		void ChangeState(object sender, StateTrigger stateTrigger);
	}
}