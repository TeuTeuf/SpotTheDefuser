using System.Collections;
using Main.Domain.Network;
using Main.Infrastructure.Controllers.Network;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Zenject;
using Zenject.Internal;

namespace Test.TestsPlayMode.Infrastructure.Network
{
    public class SpotTheDefuserNetworkManagerTest : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator Host_ShouldStartNetwork()
        {
            yield return LoadScene("TestScene");
            
            // Given 
            var spotTheDefuserNetworkManager = SceneContainer.Resolve<ISpotTheDefuserNetworkManager>();
            var networkManager = SceneContainer.Resolve<NetworkManager>();
            
            // When
            spotTheDefuserNetworkManager.Host();

            yield return new WaitForSeconds(1.0f);
            
            // Then
            Assert.IsTrue(networkManager.isNetworkActive);
            Assert.IsTrue(networkManager.IsClientConnected());
            
            networkManager.StopHost();
            yield return new WaitForSeconds(1.0f);
            Assert.That(networkManager.isNetworkActive, Is.False);
            
            // Given
            const string hostAddress = "123.123.123.123";
            
            // When
            spotTheDefuserNetworkManager.Join(hostAddress);

            yield return new WaitForSeconds(1.0f);
            
            // Then
            Assert.That(networkManager.networkAddress, Is.EqualTo(hostAddress));
            Assert.That(networkManager.isNetworkActive, Is.True);
        }
    }
}