using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.Editor.UseCases.DefuseAttempts
{
    [TestFixture]
    public class TryToDefuseTest
    {
        private IDefusingListener _defusingListener;
        private DefusingState _defusingState;
        private TryToDefuse _tryToDefuse;

        [SetUp]
        public void Init()
        {
            _defusingListener = Substitute.For<IDefusingListener>();
            _defusingState = Substitute.For<DefusingState>();

            _tryToDefuse = new TryToDefuse(_defusingState, _defusingListener); 
        }
        
        [Test]
        public void Try_ShouldNotifyDefusingListenerWithTrueAndDefusingPlayer_WhenPlayerIsDefuser()
        {
            // Given
            var player = new Player("Player");

            _defusingState.IsCurrentAttemptDefuser(player)
                .Returns(true);

            // When
            _tryToDefuse.Try(player);

            // Then
            _defusingListener.Received().OnDefuseTried(true, player);
        }
        
        [Test]
        public void Try_ShouldNotifyDefusingListenerWithFalseAndDefusingPlayer_WhenPlayerIsNotDefuser()
        {
            // Given
            var player = new Player("Player");

            _defusingState.IsCurrentAttemptDefuser(player)
                .Returns(false);

            // When
            _tryToDefuse.Try(player);

            // Then
            _defusingListener.Received().OnDefuseTried(false, player);
        }
    }
}