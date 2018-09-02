//using Main.Domain.Players;
//using Main.Infrastructure.Controllers.Network;
//using NSubstitute;
//using NUnit.Framework;
//
//namespace Test.Editor.Infrastructure.Controllers.Network
//{
//    [TestFixture]
//    public class AllPlayersControllersTest
//    {
//        private IPlayerController _localPlayerController;
//        
//        private AllPlayerControllers _allPlayerControllers;
//
//        [SetUp]
//        public void Init()
//        {
//            _localPlayerController = Substitute.For<IPlayerController>();
//            
//            _allPlayerControllers = new AllPlayerControllers {LocalPlayerController = _localPlayerController};
//        }
//        
//        [Test]
//        public void SetNewDefuseAttemptOnServer_ShouldExecuteCmdSetNewDefuseAttempt_OnLocalPlayerController()
//        {
//            // When
//            _allPlayerControllers.SetNewDefuseAttemptOnServer();
//
//            // Then
//            _localPlayerController.Received().CmdSetNewDefuseAttempt();
//        }
//
//        [Test]
//        public void AddNewPlayerOnServer_ShouldExecuteCmdAddNewPlayer_OnLocalPlayerController()
//        {
//            // Given
//            const string playerName = "Player Name";
//
//            // When
//            _allPlayerControllers.AddNewPlayerOnServer(playerName);
//
//            // Then
//            _localPlayerController.Received().CmdAddNewPlayer(playerName);
//        }
//
//        [Test]
//        public void TryToDefuseOnServer_ShouldExecuteCmdTryToDefuse_OnLocalPlayerController()
//        {
//            // When
//            _allPlayerControllers.TryToDefuseOnServer();
//            
//            // Then
//            _localPlayerController.Received().CmdTryToDefuse();
//        }
//        
//        [Test]
//        public void OnDefuseTried_ShouldExecuteRpcOnDefuseTried_OnAllPlayerControllers()
//        {
//            // Given
//            var playerControllerOnServer1 = Substitute.For<IPlayerController>();
//            _allPlayerControllers.AddPlayerControllerOnServer(playerControllerOnServer1);
//            
//            var playerControllerOnServer2 = Substitute.For<IPlayerController>();
//            _allPlayerControllers.AddPlayerControllerOnServer(playerControllerOnServer2);
//
//            var player = new Player("Player");
//            
//            // When
//            _allPlayerControllers.OnDefuseTried(true, player);
//            
//            // Then
//            playerControllerOnServer1.Received().RpcOnDefuseTried(true, player);
//            playerControllerOnServer2.Received().RpcOnDefuseTried(true, player);
//        }
//    }
//}