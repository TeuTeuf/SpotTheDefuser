using Main.Infrastructure;
using NUnit.Framework;

namespace Test.TestsEditMode.Infrastructure
{
    [TestFixture]
    public class RandomSTDTest
    {
        [Test]
        public void Range_ShouldReturnValueBetweenIncludedMinAndExcludedMax()
        {
            // Given
            var randomStd = new RandomSTD();
            const int includedMin = 0;
            const int excludedMax = 3;
                
            // When
            var result = randomStd.Range(includedMin, excludedMax);
            
            // Then
            Assert.That(result, Is.GreaterThanOrEqualTo(includedMin));
            Assert.That(result, Is.LessThan(excludedMax));
        }
    }
}