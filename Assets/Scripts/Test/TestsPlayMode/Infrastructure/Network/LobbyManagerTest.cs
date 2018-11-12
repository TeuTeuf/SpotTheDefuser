using System.Collections;
using Main.Domain.Network;
using Main.Infrastructure.Controllers.Network;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;

namespace Test.TestsPlayMode.Infrastructure.Network
{
    public class LobbyManagerTest : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator Host_ShouldStartNetwork()
        {
            yield return LoadScene("TestScene");
            
            // Given 
            var lobbyManager = SceneContainer.Resolve<ILobbyManager>();
            var networkManager = SceneContainer.Resolve<NetworkManager>();
            
            yield return null;
            
            // When
            lobbyManager.Host();

            yield return null;
            
            // Then
            Assert.IsTrue(networkManager.isNetworkActive);
            Assert.IsTrue(networkManager.IsClientConnected());
        }
    }
}