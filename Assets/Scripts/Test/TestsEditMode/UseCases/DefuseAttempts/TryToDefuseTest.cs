using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.UseCases.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.DefuseAttempts
{
    [TestFixture]
    public class TryToDefuseTest
    {
        private IDefuseSucceededListener _defuseSucceededListener;
        private IDefuseFailedListener _defuseFailedListener;
        private DefusingState _defusingState;
        private TryToDefuse _tryToDefuse;

        [SetUp]
        public void Init()
        {
            _defuseSucceededListener = Substitute.For<IDefuseSucceededListener>();
            _defuseFailedListener = Substitute.For<IDefuseFailedListener>();
            _defusingState = Substitute.For<DefusingState>();

            _tryToDefuse = new TryToDefuse(_defusingState, _defuseSucceededListener, _defuseFailedListener); 
        }

        [Test]
        public void Try_ShouldNotifyOnDefuseSucceededListener_WhenPlayerIsDefuser()
        {
            // Given
            var player = new Player("Player");

            _defusingState.IsCurrentAttemptDefuser(player)
                .Returns(true);

            // When
            _tryToDefuse.Try(player);

            // Then
            _defuseSucceededListener
                .Received()
                .OnDefuseSucceeded();
        }

        [Test]
        public void Try_ShouldNOTNotifyOnDefuseFailedListener_WhenPlayerIsDefuser()
        {
            // Given
            var player = new Player("Player");

            _defusingState.IsCurrentAttemptDefuser(player)
                .Returns(true);

            // When
            _tryToDefuse.Try(player);

            // Then
            _defuseFailedListener
                .DidNotReceive()
                .OnDefuseFailed();
        }

        [Test]
        public void Try_ShouldNOTNotifyOnDefuseSucceededListener_WhenPlayerIsNOTDefuser()
        {
            // Given
            var player = new Player("Player");

            _defusingState.IsCurrentAttemptDefuser(player)
                .Returns(false);

            // When
            _tryToDefuse.Try(player);

            // Then
            _defuseSucceededListener
                .DidNotReceive()
                .OnDefuseSucceeded();
        }

        [Test]
        public void Try_ShouldNotifyOnDefuseFailedListener_WhenPlayerIsNOTDefuser()
        {
            // Given
            var player = new Player("Player");

            _defusingState.IsCurrentAttemptDefuser(player)
                .Returns(false);

            // When
            _tryToDefuse.Try(player);

            // Then
            _defuseFailedListener
                .Received()
                .OnDefuseFailed();
        }
    }
}