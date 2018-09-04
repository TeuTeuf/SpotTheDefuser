using NUnit.Framework;

namespace Test.TestsEditMode
{
    [TestFixture]
    public class FailingTest
    {
        [Test]
        public void ThisIsAFailingTest()
        {
            Assert.IsTrue(false);
        }
    }
}