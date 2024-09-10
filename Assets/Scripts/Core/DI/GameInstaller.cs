using InteriorBuilderTest.Core.ECS;
using InteriorBuilderTest.Core.StateMachine;
using Zenject;

namespace InteriorBuilderTest
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			MainStateMachineInstaller.Install(Container);
			EcsInstaller.Install(Container);
		}
	}
}