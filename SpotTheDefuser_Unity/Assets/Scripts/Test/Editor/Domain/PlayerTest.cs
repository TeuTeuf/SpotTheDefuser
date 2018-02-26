using NUnit.Framework;

public class PlayerTest {
	[Test]
	public void NewPlayer_shouldHaveAPseudo() {
		// Given
		// When
		Player player = new Player ("Test");

		// Then
		Assert.That(player.pseudo, Is.EqualTo("Test"));
	}
}
