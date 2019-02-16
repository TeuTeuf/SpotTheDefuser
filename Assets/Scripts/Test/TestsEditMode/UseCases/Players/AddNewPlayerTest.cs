using Main.Domain.Players;
using Main.UseCases.Players;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.Players
{
    [TestFixture]
    public class AddNewPlayerTest
    {
        private AllPlayers _allPlayers;
        private IPlayerAddedListener _playerAddedListener;
        private AddNewPlayer _addNewPlayer;
        
        private Player _aPlayer = new Player("Test");

        [SetUp]
        public void Init()
        {
            _allPlayers = Substitute.For<AllPlayers>();
            _playerAddedListener = Substitute.For<IPlayerAddedListener>();
            _addNewPlayer = new AddNewPlayer(_allPlayers, _playerAddedListener);
        }
        
        [Test]
        public void Execute_ShouldAddPlayerToAllPlayers()
        {
            // When
            _addNewPlayer.Execute(_aPlayer);

            // Then
            _allPlayers
                .Received()
                .Add(_aPlayer);
        }

        [Test]
        public void Execute_ShouldNotifyListenerWithNewPlayerAdded()
        {
            // When
            _addNewPlayer.Execute(_aPlayer);

            // Then
            _playerAddedListener
                .Received()
                .OnPlayerAdded(_aPlayer);
        }
    }
}
