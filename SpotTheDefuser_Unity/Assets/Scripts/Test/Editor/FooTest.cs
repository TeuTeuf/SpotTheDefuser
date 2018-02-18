using NUnit.Framework;
using NSubstitute;

public class FooTest {

	[Test]
	public void IsOk_shouldReturnTrue() {
        Foo foo = new Foo();
        Bar bar = Substitute.For<Bar>();

        bar.IsReallyOk().Returns(true);

        Assert.AreEqual(true, foo.IsOk());
        Assert.AreEqual(true, bar.IsReallyOk());
	}

}
