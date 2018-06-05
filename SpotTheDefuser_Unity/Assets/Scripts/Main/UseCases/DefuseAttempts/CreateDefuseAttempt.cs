using System.Collections.Generic;
using Main.Domain;
using Main.Domain.DefuseAttempts;
using Main.Domain.Players;

namespace Main.UseCases.DefuseAttempts
{
	public class CreateDefuseAttempt {
		private readonly IRandom _random;
		private readonly IPlayerRepository _playerRepository;

		public CreateDefuseAttempt(IRandom random, IPlayerRepository playerRepository)
		{
			_random = random;
			_playerRepository = playerRepository;
		}

		public DefuseAttempt Execute()
		{
			var allPlayers = new List<Player>(_playerRepository.GetAll());
			var defuserPlayers = new List<Player> {allPlayers[_random.Range(0, allPlayers.Count)]};
			return new DefuseAttempt(defuserPlayers);
		}
	}
}
