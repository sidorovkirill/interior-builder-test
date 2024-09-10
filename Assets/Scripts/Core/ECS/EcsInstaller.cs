using Core.DI;
using Zenject;

namespace InteriorBuilderTest.Core.ECS
{
	public class EcsInstaller : Installer<EcsInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<EcsSystemsFactory>().AsSingle();
			Container.BindInterfacesTo<EcsStartup>().AsSingle();
		}
	}
}