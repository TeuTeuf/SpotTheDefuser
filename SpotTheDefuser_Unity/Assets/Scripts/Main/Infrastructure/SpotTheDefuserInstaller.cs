using Main.Domain;
using Main.Domain.Players;
using Main.Infrastructure.Players;
using Main.UseCases.Players;
using Zenject;

namespace Main.Infrastructure
{
    public class SpotTheDefuserInstaller : MonoInstaller<SpotTheDefuserInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerRepository>().To<PlayerRepository>().AsSingle();

            Container.Bind<IRandom>().To<RandomSTD>().AsSingle();
            
            // Use Cases
            Container.Bind<AddNewPlayer>().AsSingle();
            Container.Bind<GetAllPlayers>().AsSingle();
            Container.Bind<RemovePlayer>().AsSingle();
        }
    }
}