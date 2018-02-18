using NUnit.Framework;
using NSubstitute;

public class FooTest {

	[Test]
	public void IsOk_shouldReturnTrue() {
        Foo foo = new Foo();
        Bar bar = Substitute.For<Bar>();

        bar.IsReallyOk().Returns(false);

        Assert.That(foo.IsOk(), Is.True);
        Assert.That(bar.IsReallyOk(), Is.False);
        bar.Received().IsReallyOk();
	}

}
