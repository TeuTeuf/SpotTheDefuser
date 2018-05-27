using Main.Domain.DefuseAttempts;
using Main.UseCases.DefuseAttempts;
using NUnit.Framework;

namespace Test.Editor.UseCases.DefuseAttempts
{
    public class CreateDefuseAttemptTest {

        [Test]
        public void Execute_ShouldReturnADefuseAttempt()
        {
            // Given
            var createDefuseAttempt = new CreateDefuseAttempt();
            
            // When
            var defuseAttempt = createDefuseAttempt.Execute();

            // Then
            Assert.IsInstanceOf<DefuseAttempt>(defuseAttempt);
        }
    }
}
