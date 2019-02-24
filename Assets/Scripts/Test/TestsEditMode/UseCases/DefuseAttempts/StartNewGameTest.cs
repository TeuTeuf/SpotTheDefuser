using Main.Domain.DefuseAttempts;
using Main.UseCases.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.DefuseAttempts
{
    [TestFixture]
    public class StartNewGameTest
    {
        private INewGameStartedListener _newGameStartedListener;
        private StartNewGame _startNewGame;

        [SetUp]
        public void Init()
        {
            _newGameStartedListener = Substitute.For<INewGameStartedListener>();
            _startNewGame = new StartNewGame(_newGameStartedListener);
        }
        
        [Test]
        public void Start_ShouldTriggerNewGameStartedListener()
        {
            // When
            _startNewGame.Start();

            // Then
            _newGameStartedListener
                .Received()
                .OnNewGameStarted();
        }
    }
}