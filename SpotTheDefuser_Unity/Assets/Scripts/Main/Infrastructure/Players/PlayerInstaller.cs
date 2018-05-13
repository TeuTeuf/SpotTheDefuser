using Main.Domain.Players;
using Main.UseCases.Players;
using Zenject;

namespace Main.Infrastructure.Players
{
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
}