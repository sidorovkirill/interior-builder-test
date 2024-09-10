using System.Linq;
using InteriorBuilderTest.Core.StateMachine.States;
using Zenject;

namespace InteriorBuilderTest.Core.StateMachine
{
	public class MainStateMachineInstaller : Installer<MainStateMachineInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<StateRequester>().AsSingle();
			Container.BindInterfacesTo<MainStateMachine>().AsSingle();
			Container.Bind<InitialState>().AsSingle();
			Container.Bind<MainState>().AsSingle();
		}
	}
}