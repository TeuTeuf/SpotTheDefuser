using Main.Domain;
using Main.Domain.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.Domain.DefuseAttempts
{
    [TestFixture]
    public class AllBombsTest
    {
        private AllBombs _allBombs;
        
        private IBomb[] _bombs;
        private IRandom _random;

        [SetUp]
        public void Init()
        {
            var bomb1 = Substitute.For<IBomb>();
            bomb1.Id.Returns("bomb1");
            
            var bomb2 = Substitute.For<IBomb>();
            bomb2.Id.Returns("bomb2");
            
            var bomb3 = Substitute.For<IBomb>();
            bomb3.Id.Returns("bomb3");
            
            _bombs = new[] {bomb1, bomb2, bomb3};
            _random = Substitute.For<IRandom>();
            _allBombs = new AllBombs(_random, _bombs);
        }

        [Test]
        public void PickRandomBombId_ShouldReturnABombIdPickedWithRandom()
        {
            // Given
            _random.Range(0, _bombs.Length)
                .Returns(1);

            // When
            var randomBombId = _allBombs.PickRandomBombId();

            // Then
            Assert.That(randomBombId, Is.EqualTo("bomb2"));
        }
    }
}