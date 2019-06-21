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
        private IDeviceInfo _deviceInfo;

        [SetUp]
        public void Init()
        {
            var bomb1 = Substitute.For<IBomb>();
            bomb1.Id.Returns("bomb1");
            bomb1.Language.Returns(BombLanguage.NONE);
            
            var bomb2 = Substitute.For<IBomb>();
            bomb2.Id.Returns("bomb2");
            bomb2.Language.Returns(BombLanguage.NONE);
            
            var bomb3 = Substitute.For<IBomb>();
            bomb3.Id.Returns("bomb3");
            bomb3.Language.Returns(BombLanguage.NONE);
            
            var bomb4_en = Substitute.For<IBomb>();
            bomb4_en.Id.Returns("bomb4_en");
            bomb4_en.Language.Returns(BombLanguage.ENGLISH);
            
            var bomb4_fr = Substitute.For<IBomb>();
            bomb4_fr.Id.Returns("bomb4_fr");
            bomb4_fr.Language.Returns(BombLanguage.FRENCH);
            
            _bombs = new[] {bomb1, bomb2, bomb3, bomb4_en, bomb4_fr};
            _random = Substitute.For<IRandom>();
            _deviceInfo = Substitute.For<IDeviceInfo>();
        }

        [Test]
        public void PickRandomBombId_ShouldReturnABombIdPickedWithRandom()
        {
            // Given
            _random.Range(0, 3)
                .Returns(1);

            _deviceInfo.GetDeviceBombLanguage()
                .Returns(BombLanguage.NONE);
            
            _allBombs = new AllBombs(_random, _bombs, _deviceInfo);

            // When
            var randomBombId = _allBombs.PickRandomBombId();

            // Then
            Assert.That(randomBombId, Is.EqualTo("bomb2"));
        }

        [Test]
        public void PickRandomBombId_ShouldReturnABombFilteredByItsLanguage()
        {
            // Given
            _random.Range(0, 4)
                .Returns(3);

            _deviceInfo.GetDeviceBombLanguage()
                .Returns(BombLanguage.FRENCH);
            
            _allBombs = new AllBombs(_random, _bombs, _deviceInfo);

            // When
            var randomBombId = _allBombs.PickRandomBombId();

            // Then
            Assert.That(randomBombId, Is.EqualTo("bomb4_fr"));
        }

        [Test]
        public void GetByBombId_ReturnBombCorrespondingToId()
        {
            // Given
            var expectedBomb = _bombs[1];
            var idExpectedBomb = expectedBomb.Id;
            
            _allBombs = new AllBombs(_random, _bombs, _deviceInfo);
            
            // When
            var bomb = _allBombs.GetByBombId(idExpectedBomb);

            // Then
            Assert.That(bomb, Is.EqualTo(expectedBomb));
        }
    }
}