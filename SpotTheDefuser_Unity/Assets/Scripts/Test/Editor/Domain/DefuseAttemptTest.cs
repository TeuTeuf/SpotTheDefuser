using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;

namespace SpotTheDefuser_Unity.Editor.Assets.Scripts.Test.Editor.Domain
{
    public class DefuseAttemptTest
    {
        Player player1 = new Player("player1");
        Player player2 = new Player("player2");
        Player player3 = new Player("player3");

        IList<Player> listPlayer;
        IRandom random;

        [SetUp]
        public void Setup()
        {
            listPlayer = new List<Player>();

            listPlayer.Add(player1);
            listPlayer.Add(player2);
            listPlayer.Add(player3);

            random = Substitute.For<IRandom>();
        }

        [Test]
        public void IsDefuser_ShouldReturnTrueIfRandomValueIsEqualToPlayerIndex()
        {
            random.Range(0, listPlayer.Count)
                  .Returns(0);

            DefuseAttempt defuseAttempt = new DefuseAttempt(random, listPlayer);

            // When
            bool isPlayer1Defuser = defuseAttempt.IsDefuser(player1);

            // Then
            Assert.That(isPlayer1Defuser, Is.True);
        }

        [Test]
        public void IsDefuser_ShouldReturnFalseIfRandomValueIsNotEqualToPlayerIndex()
        {
            random.Range(0, listPlayer.Count)
                  .Returns(1);

            DefuseAttempt defuseAttempt = new DefuseAttempt(random, listPlayer);

            // When
            bool isPlayer1Defuser = defuseAttempt.IsDefuser(player1);

            // Then
            Assert.That(isPlayer1Defuser, Is.False);
        }
    }
}
