using Main.Domain.Network;
using Main.UseCases.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Network
{
    public class HostNewGameTest
    {
        private HostNewGame _hostNewGame;
        private ILobbyManager _lobbyManager;
        
        [SetUp]
        public void Init()
        {
            _lobbyManager = Substitute.For<ILobbyManager>();
            _hostNewGame = new HostNewGame(_lobbyManager);
        }

        [Test]
        public void Host_ShouldStartHostingOnNetworkManager()
        {
            // When
            _hostNewGame.Host();

            // Then
            _lobbyManager.Received().Host();
        }
    }
}