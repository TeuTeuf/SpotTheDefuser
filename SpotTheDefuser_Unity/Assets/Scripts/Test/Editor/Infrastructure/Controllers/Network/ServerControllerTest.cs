using Main.Infrastructure.Controllers.Network;
using NUnit.Framework;
using UnityEngine;

namespace Test.Editor.Infrastructure.Controllers.Network
{
    [TestFixture]
    public class ServerControllerTest
    {
        [Test]
        public void Awake_ShouldSetServerControllerInstanceInNetworkControllers()
        {
            // Given
            var networkControllers = new NetworkControllers();
            
            var serverController = new GameObject().AddComponent<ServerController>();
            serverController.NetworkControllers = networkControllers;
            
            // When
            serverController.Awake();
            
            // Then
            Assert.AreSame(networkControllers.ServerController, serverController);
        }
    }
}