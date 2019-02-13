using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.Infrastructure.Network;
using Main.UseCases.DefuseAttempts;
using Main.UseCases.Network;
using Main.UseCases.Players;
using Main.UseCases.UI;
using Zenject;

namespace Main.Infrastructure
{
    public class SpotTheDefuserInstaller : MonoInstaller<SpotTheDefuserInstaller>
    {
        public override void InstallBindings()
        {
            InstallInterfaceImplementations();
            InstallUseCases();
            InstallOtherSingletons();
            InstallFromComponentInHierarchy();
        }

        private void InstallInterfaceImplementations()
        {
            Container.Bind<IRandom>().To<SpotTheDefuserRandom>().AsSingle();
            Container.Bind<IDefusingListener>().To<AllPlayerControllers>().FromResolve();
            Container.Bind<IViewManager>().To<ViewManager>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<AddNewPlayer>().AsSingle();
            Container.Bind<GetAllPlayers>().AsSingle();
            Container.Bind<RemovePlayer>().AsSingle();
            Container.Bind<SetNewDefuseAttempt>().AsSingle();
            Container.Bind<TryToDefuse>().AsSingle();
            Container.Bind<ChangeCurrentView>().AsSingle();
            Container.Bind<HostNewGame>().AsSingle();
            Container.Bind<StartWaitingForNewGame>().AsSingle();
            Container.Bind<ConnectToNewGame>().AsSingle();
        }

        private void InstallOtherSingletons()
        {
            Container.Bind<AllPlayers>().AsSingle();
            Container.Bind<DefusingState>().AsSingle();
            Container.Bind<AllPlayerControllers>().AsSingle();
        }

        private void InstallFromComponentInHierarchy()
        {
            Container.Bind<UIController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IViewLayer>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<ISpotTheDefuserNetworkManager>().To<SpotTheDefuserNetworkManager>().FromComponentInHierarchy().AsSingle();;
            Container.Bind<ISpotTheDefuserNetworkDiscovery>().FromComponentInHierarchy().AsSingle();
        }
    }
}