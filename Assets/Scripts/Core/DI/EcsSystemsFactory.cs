using Leopotam.EcsLite;
using Zenject;

namespace Core.DI
{
	public class EcsSystemsFactory
	{
		private readonly DiContainer _container;

		public EcsSystemsFactory(DiContainer diContainer)
		{
			_container = diContainer;
		}

		public T Create<T>() where T : class, IEcsSystem
		{
			var instance = _container.TryResolve<T>();
			if (instance == null)
			{
				instance = _container.Instantiate<T>();
			}
			return instance;
		}
	}
}