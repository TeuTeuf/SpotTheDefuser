using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Infrastructure;
using SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases;
using Zenject;

public class PlayerInstaller : MonoInstaller<PlayerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayersRepository>().To<LocalPlayersRepository>().AsSingle();

        // Use Cases
        Container.Bind<AddNewPlayer>().AsSingle();
        Container.Bind<GetAllPlayers>().AsSingle();
        Container.Bind<RemovePlayer>().AsSingle();
    }
}