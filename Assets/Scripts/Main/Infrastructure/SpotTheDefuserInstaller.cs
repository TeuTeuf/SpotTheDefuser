using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Network;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Infrastructure.Controllers.Network;
using Main.Infrastructure.Data;
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
            InstallFromResources();
        }

        private void InstallInterfaceImplementations()
        {
            Container.Bind<IRandom>().To<SpotTheDefuserRandom>().AsSingle();
            Container.Bind<IViewManager>().To<ViewManager>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<AddNewPlayer>().AsSingle();
            Container.Bind<StartNewGame>().AsSingle();
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
            Container.Bind<AllBombs>().AsSingle();
            Container.Bind<DefusingState>().AsSingle();
            Container.Bind<DefuserCounter>().AsSingle();
            Container.Bind<AllPlayerControllers>().AsSingle();
            Container.Bind<NetworkBehaviourChecker>().AsSingle();
            Container.Bind<IPlayerAddedListener>().To<AllPlayerControllers>().FromResolve();
            Container.Bind<INewGameStartedListener>().To<AllPlayerControllers>().FromResolve();
            Container.Bind<IDefuseTriedListener>().To<AllPlayerControllers>().FromResolve();
            Container.Bind<INewDefuseAttemptSetListener>().To<AllPlayerControllers>().FromResolve();
        }

        private void InstallFromComponentInHierarchy()
        {
            Container.Bind<IUIController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ILobbyLayer>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IViewLayer>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<ISpotTheDefuserNetworkManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ISpotTheDefuserNetworkDiscovery>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallFromResources()
        {
            Container.Bind<Bomb>().FromScriptableObjectResource("Bombs").AsSingle();
            Container.Bind<IBomb>().To<Bomb>().FromResolveAll();
        }
    }
}