using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.UseCases.DefuseAttempts;
using NSubstitute;
using NUnit.Framework;

namespace Test.TestsEditMode.UseCases.DefuseAttempts
{
    [TestFixture]
    public class InitDefusingTest
    {
        private DefusingState _defusingState;
        
        private InitDefusing _initDefusing;

        [SetUp]
        public void Init()
        {
            _defusingState = Substitute.For<DefusingState>(
                Substitute.For<IDefusingTime>(),
                Substitute.For<IDefusingTimerUpdatedListener>(),
                Substitute.For<IDefuseFailedListener>()
            );
            _initDefusing = new InitDefusing(_defusingState);
        }
        
        [Test]
        public void Init_ShouldStartNewDefusingTimer()
        {
            // When
            _initDefusing.Init();

            // Then
            _defusingState
                .Received()
                .StartNewTimer();
        }
    }
}