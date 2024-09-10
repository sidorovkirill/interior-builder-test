using InteriorBuilderTest.DTO;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigsInstaller", menuName = "Installers/GameConfigsInstaller")]
public class GameConfigsInstaller : ScriptableObjectInstaller<GameConfigsInstaller>
{
    [SerializeField] private AvailableAssets _availableAssets;
    [SerializeField] private BuildingConfig _buildingConfig;
    [SerializeField] private MaterialConfig _materialConfig;
    public override void InstallBindings()
    {
        Container.BindInstance(_availableAssets);
        Container.BindInstance(_buildingConfig);
        Container.BindInstance(_materialConfig);
    }
}