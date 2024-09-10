using System;
using Core.DI;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using InteriorBuilderTest.Components;
using InteriorBuilderTest.Systems.Input;
using InteriorBuilderTest.Systems.UI;
using Systems;
using Zenject;

namespace InteriorBuilderTest.Core.ECS
{
	public class EcsStartup : IInitializable, IDisposable, ITickable
	{
		private EcsWorld _world;
		private EcsSystems _systems;
		
		private EcsSystemsFactory _factory;

		public EcsStartup(EcsSystemsFactory EcsSystemsFactory)
		{
			_factory = EcsSystemsFactory;
		}

		public void Initialize()
        {
	        _world = new EcsWorld();
	        _systems = new EcsSystems(_world);

			_systems
				.Add(_factory.Create<ButtonsInputSystem>())
				.Add(_factory.Create<MouseScrollSystem>())
				.Add(_factory.Create<RaycastSystem>())
				.Add(_factory.Create<FurniturePanelSystem>())
				.Add(_factory.Create<CursorControlSystem>())
				.Add(_factory.Create<FurnitureBuilderSystem>())
				.Add(_factory.Create<PositioningNewObjSystem>())
				.Add(_factory.Create<ObjectDropSystem>())
				.Add(_factory.Create<MaterialSwapSystem>())
				.Add(_factory.Create<FurnitureRotationSystem>())

				.DelHere<Hit>()
				.DelHere<Miss>()
				.DelHere<ObjectDropped>()
				.DelHere<FurniturePanelToggled>()
				.DelHere<PanelClosed>()
				.DelHere<FurnitureCreated>()
				.DelHere<MouseScrolled>()
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
				.Inject()
				.Init();
		}

		public void Tick()
		{
			_systems.Run();
		}

        public void Dispose()
		{
			if (_systems == null) return;
			_systems.Destroy ();
			_systems = null;
			_world.Destroy ();
			_world = null;
		}
	}
}