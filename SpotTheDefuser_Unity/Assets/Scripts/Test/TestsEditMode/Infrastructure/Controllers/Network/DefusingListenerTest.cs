using Main.Domain.Players;
using Main.Infrastructure.Controllers.Network;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.Infrastructure.Controllers.Network
{
    [TestFixture]
    public class DefusingListenerTest
    {
        private DefusingListener _defusingListener;
        private AllPlayerControllers _allPlayerControllers;

        [SetUp]
        public void Init()
        {
            _allPlayerControllers = Substitute.For<AllPlayerControllers>();
            _defusingListener = new DefusingListener(_allPlayerControllers);
        }

        [Test]
        public void OnDefuseTried_ShouldCallRpcOnDefuseTried_OnUIController()
        {
            // Given
            var player = new Player("player");
            
            // When
            _defusingListener.OnDefuseTried(true, player);
            
            // Then
            _allPlayerControllers.Received().OnDefuseTried(true, player);
        }
    }
}